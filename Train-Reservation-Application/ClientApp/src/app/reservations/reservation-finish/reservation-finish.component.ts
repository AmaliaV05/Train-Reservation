import { Component, OnDestroy, OnInit } from '@angular/core';
import { NewReservationRequest, ResponseService, SeatInCarViewModel, TicketViewModel } from '../reservations.model';
import { ReservationsService } from '../reservations.service';
import { Subscription } from 'rxjs';
import { DataService } from '../../trains/data.service';
import { CarType } from '../../trains/train.model';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core/error/error-options';
import { Router } from '@angular/router';

export class MyErrorStateMatcher implements ErrorStateMatcher {
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
  ticket: TicketViewModel = {
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
            type: CarType.All,
            train: {
              id: 0,
              name: ''
            }
          }
        }]
      }]
  };
  occupiedSeats: SeatInCarViewModel[] = [{
    id: 0,
    number: 0,
    car: {
      id: 0,
      carNumber: 0,
      type: CarType.All,
      train: {
        id: 0,
        name: ''
      }
    }
  }];

  reservationForm = new FormGroup({
    socialSecurityNumber: new FormControl('', Validators.required),
    name: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email])
  });

  matcher = new MyErrorStateMatcher();
  showTicket = false;
  message: string;
  reservationDate: Date;
  reserveSeatsIds = new Array<number>();
  reservationId: number;
  subscription: Subscription;
  seatsListSubscription: Subscription;
  reservationIdSubscription: Subscription;

  constructor(private reservationService: ReservationsService,
    private dataService: DataService,
    private router: Router) { }

  ngOnInit() {
    this.subscription = this.dataService.currentMessage$.subscribe(message =>
      this.reservationDate = message);
    this.seatsListSubscription = this.dataService.currentSeatListMessage$.subscribe(message =>
      this.reserveSeatsIds = message);
    this.reservationIdSubscription = this.dataService.currentModifyReservationMessage$.subscribe(message =>
      this.reservationId = message);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    this.seatsListSubscription.unsubscribe();
  }

  postReservation() {
    this.newReservation = {
      socialSecurityNumber: this.reservationForm.get('socialSecurityNumber').value,
      name: this.reservationForm.get('name').value,
      email: this.reservationForm.get('email').value,
      reservationWithSeatsViewModel: {
        reservationDate: this.reservationDate,
        reservedSeatsIds: this.reserveSeatsIds
      }
    };
    this.reservationService.makeReservation(this.newReservation)
      .subscribe((response: ResponseService<TicketViewModel, SeatInCarViewModel[], string>) => {
        this.ticket = response.response;
        this.occupiedSeats = response.alternativeResponse;
        this.message = response.message;
        this.reservationId = this.ticket.reservations[0].id;
        this.dataService.getReservationId(this.reservationId);
      });
    this.reservationForm.reset();
    this.showTicket = true;
  }

  goToModifyReservation() {
    this.router.navigateByUrl('modify-reservation');
  }
}
