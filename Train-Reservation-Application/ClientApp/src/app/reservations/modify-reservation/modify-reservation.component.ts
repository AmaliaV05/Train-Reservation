import { Component, ViewChild } from "@angular/core";
import { FormControl, NgForm, Validators } from "@angular/forms";
import { Subscription } from "rxjs";
import { DataService } from "../../trains/data.service";
import { CarType } from "../../trains/train.model";
import { ModifyReservationViewModel, ResponseService, SeatInCarViewModel, TicketViewModel } from "../reservations.model";
import { ReservationsService } from "../reservations.service";

@Component({
  selector: 'app-modify-reservation',
  templateUrl: './modify-reservation.component.html'
})
export class ModifyReservationComponent {
  modifiedReservation: ModifyReservationViewModel = {
    id: 0,
    code: '',
    reservationDate: new Date,
    reservedSeatsIds: new Array<number>()
  };
  code: FormControl;
  selectedDate: Date;
  reserveSeatsIds = new Array<number>();
  reservationId: number;
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
  message: string;
  showTicket = false;
  subscription: Subscription;
  seatsListSubscription: Subscription;
  reservationIdSubscription: Subscription;

  @ViewChild("modifyReservationForm") modifyReservationForm!: NgForm;

  constructor(private dataService: DataService,
    private reservationsService: ReservationsService) { }

  ngOnInit() {
    this.subscription = this.dataService.currentMessage$.subscribe(message =>
      this.selectedDate = message);
    this.seatsListSubscription = this.dataService.currentSeatListMessage$.subscribe(message =>
      this.reserveSeatsIds = message);
    this.reservationIdSubscription = this.dataService.currentModifyReservationMessage$.subscribe(message =>
      this.reservationId = message);
    this.code = new FormControl('', [Validators.required, Validators.minLength(7), Validators.maxLength(7)]);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    this.seatsListSubscription.unsubscribe();
  }

  modifyReservation() {
    this.modifiedReservation = {
      id: this.reservationId,
      code: this.code.value,
      reservationDate: this.selectedDate,
      reservedSeatsIds: this.reserveSeatsIds
    };
    this.reservationsService.modifyReservation(this.reservationId, this.modifiedReservation)
      .subscribe((response: ResponseService<TicketViewModel, SeatInCarViewModel[], string>) => {
        this.ticket = response.response;
        this.occupiedSeats = response.alternativeResponse;
        this.message = response.message;
      });
    this.reserveSeatsIds = [];
    this.modifyReservationForm.resetForm();
    this.showTicket = true;
  }
}
