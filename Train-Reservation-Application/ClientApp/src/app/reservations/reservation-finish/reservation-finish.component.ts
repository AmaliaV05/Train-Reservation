import { Component, OnDestroy, OnInit } from '@angular/core';
import { DataService } from '../../home/data.service';
import { NewReservationRequestViewModel } from '../reservations.model';
import { ReservationsService } from '../reservations.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-reservation-finish',
  templateUrl: './reservation-finish.component.html',
  styleUrls: ['./reservation-finish.component.css'],
})
export class ReservationFinishComponent implements OnInit, OnDestroy {

  newReservation: NewReservationRequestViewModel;
  code: string;
  reservationDate: string;
  subscription: Subscription;

  constructor(private reservationService: ReservationsService,
    private data: DataService) { }

  ngOnInit() {
    this.subscription = this.data.currentMessage.subscribe(message =>
      this.reservationDate = message);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  postReservation() {
    this.reservationService.post('Reservations', this.newReservation)
      .subscribe((response: string) =>
        this.code = response);
  }
}
