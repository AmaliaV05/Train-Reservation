import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core/error/error-options';
import { Router } from '@angular/router';
import { FetchResult } from '@apollo/client/core';
import { Subscription } from 'rxjs';
import { FeatureFlagService } from '../../../core/services/feature-flag.service';
import { FeatureFlags } from '../../../core/feature-flags/feature-flags.enum';
import { DataService } from '../../trains/data.service';
import { CarType } from '../../trains/enums';
import { Seat } from '../../trains/graphql/types/seat-type';
import { Customer } from '../graphql/types/customer-type';
import { ReservationMutation } from '../graphql/types/reservation-mutation';
import { ReservationsGraphqlService } from '../reservations-graphql.service';
import { NewReservationRequest, ResponseService, SeatInCarViewModel, TicketViewModel } from '../reservations.model';
import { ReservationsService } from '../reservations.service';

export class EmailErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-reservation-finish',
  templateUrl: './reservation-finish.component.html',
  styleUrls: ['./reservation-finish.component.css'],
})
export class ReservationFinishComponent implements OnInit, OnDestroy {

  newReservation: NewReservationRequest = {
      socialSecurityNumber: '',
      name: '',
      email: '',
      reservationWithSeatsViewModel: {
        reservationDate: new Date,
        reservedSeatsIds: new Array<number>()
      }
  };
  ticket: TicketViewModel | Customer = {
      name: '',
      reservations: [{
        id: 0,
        code: '',
        reservationDate: new Date,
        seats: [{
          id: 0,
          number: 0,
          car: {
            id: 0,
            carNumber: 0,
            type: CarType.ALL,
            train: {
              id: 0,
              name: ''
            }
          }
        }]
      }]
  };
  occupiedSeats: SeatInCarViewModel[] | Seat[] = [{
    id: 0,
    number: 0,
    car: {
      id: 0,
      carNumber: 0,
      type: CarType.ALL,
      train: {
        id: 0,
        name: ''
      }
    }
  }];
  
  reservationForm: FormGroup;
  matcher = new EmailErrorStateMatcher();
  showTicket = false;
  message: string;
  reservationDate: Date;
  reserveSeatsIds = new Array<number>();
  reservationId: number;
  subscription: Subscription;
  seatsListSubscription: Subscription;
  reservationIdSubscription: Subscription;

  constructor(private reservationService: ReservationsService,
    private reservationsGraphqlService: ReservationsGraphqlService,
    private featureFlagService: FeatureFlagService,
    private dataService: DataService,
    private router: Router) { }

  ngOnInit() {
    this.subscription = this.dataService.currentMessage$.subscribe(message =>
      this.reservationDate = message);
    this.seatsListSubscription = this.dataService.currentSeatListMessage$.subscribe(message =>
      this.reserveSeatsIds = message);
    this.reservationIdSubscription = this.dataService.currentModifyReservationMessage$.subscribe(message =>
      this.reservationId = message);
    this.reservationForm = new FormGroup({
      socialSecurityNumber: new FormControl('', [Validators.required, Validators.minLength(13), Validators.maxLength(13)]),
      name: new FormControl('', [Validators.required, Validators.minLength(10)]),
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    this.seatsListSubscription.unsubscribe();
    this.reservationIdSubscription.unsubscribe();
  }

  postReservation() {
    let socialSecurityNumber = this.reservationForm.get('socialSecurityNumber').value as string;
    let name = this.reservationForm.get('name').value as string;
    let email = this.reservationForm.get('email').value as string;

    this.newReservation = {
      socialSecurityNumber: socialSecurityNumber,
      name: name,
      email: email,
      reservationWithSeatsViewModel: {
        reservationDate: this.reservationDate,
        reservedSeatsIds: this.reserveSeatsIds
      }
    };
    if (this.featureFlagService.isEnabled(FeatureFlags.UseGraphQL)) {
      this.reservationsGraphqlService.makeReservation(socialSecurityNumber, name, email, this.reservationDate, this.reserveSeatsIds)
        .subscribe((response: FetchResult<ReservationMutation>) => {
        let result = response.data?.addReservation;

        this.ticket = result.response;
        this.occupiedSeats = result.alternativeResponse;
        this.message = result.message;
        this.reservationId = this.ticket.reservations[0]?.id;
        this.dataService.getReservationId(this.reservationId);
      });
    }
    else {
      this.reservationService.makeReservation(this.newReservation)
        .subscribe((response: ResponseService<TicketViewModel, SeatInCarViewModel[], string>) => {
          this.ticket = response.response;
          this.occupiedSeats = response.alternativeResponse;
          this.message = response.message;
          this.reservationId = this.ticket.reservations[0].id;
          this.dataService.getReservationId(this.reservationId);
        });
    }
    
    this.reservationForm.reset();
    this.showTicket = true;
  }

  goToModifyReservation() {
    this.router.navigateByUrl('modify-reservation');
  }
}
