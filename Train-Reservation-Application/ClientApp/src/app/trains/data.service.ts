import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private messageSource = new BehaviorSubject<Date>(new Date);
  currentMessage$ = this.messageSource.asObservable();

  private seatListSource = new BehaviorSubject<Array<number>>(new Array<number>());
  currentSeatListMessage$ = this.seatListSource.asObservable();

  private modifyReservationIdSource = new BehaviorSubject<number>(0);
  currentModifyReservationMessage$ = this.modifyReservationIdSource.asObservable();

  constructor() { }

  getReservationDate(date: Date) {
    this.messageSource.next(date);
  }

  getSeatsIdsList(list: Array<number>) {
    this.seatListSource.next(list);
  }

  getReservationId(id: number) {
    this.modifyReservationIdSource.next(id);
  }
}
