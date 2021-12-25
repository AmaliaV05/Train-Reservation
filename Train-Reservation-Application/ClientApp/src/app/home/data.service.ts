import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  selectedDate: Date;

  private messageSource = new BehaviorSubject<Date>(new Date);
  currentMessage$ = this.messageSource.asObservable();

  constructor() { }

  getReservationDate(date: Date) {
    this.messageSource.next(date);
  }
}
