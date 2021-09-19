import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../home/data.service';
import { Train } from '../train.model';
import { TrainsService } from '../trains.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-date-filter',
  templateUrl: './date-filter.component.html'
})
export class DateFilterComponent implements OnInit, OnDestroy {

  trains: Train[];
  filterDate = new Date();
  myDate: string;
  subscription: Subscription;

  constructor(private trainService: TrainsService,
    private router: Router,
    private data: DataService) { }

  ngOnInit() {
    this.myDate = this.filterDate.toISOString().split('T')[0];
    this.getFilteredTrains(this.myDate);
    this.subscription = this.data.currentMessage.subscribe(message =>
      this.myDate = message)
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getFilteredTrains(giveDate: string) {    
    this.trainService.get(`Trains/filter-trains/${giveDate}`).subscribe((response: Train[]) =>
      this.trains = response);
  }

  newMessage() {
    this.data.getReservationDate("lala");
  }
}
