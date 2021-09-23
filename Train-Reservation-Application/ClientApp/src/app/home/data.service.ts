import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  myDate: string;

  private messageSource = new BehaviorSubject('2021-09-14');
  currentMessage = this.messageSource.asObservable();

  constructor() { }

  getReservationDate(date: string) {
    this.messageSource.next(date);
  } 

}
