import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../home/data.service';
import { Train } from '../train.model';
import { TrainsService } from '../trains.service';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-date-filter',
  templateUrl: './date-filter.component.html'
})
export class DateFilterComponent implements OnInit, OnDestroy {

  trains: Train[];
  filterDate = new Date();
  myDate: string;
  subscription: Subscription;
  myForm: FormGroup;

  constructor(private trainService: TrainsService,
    private router: Router,
    private data: DataService) { }

  ngOnInit() {
    /*this.myForm = new FormGroup({
      'presentDate': new FormControl((new Date()).toISOString().substring(0, 10)),
    });*/    
    this.myDate = this.filterDate.toISOString().split('T')[0];
    this.subscription = this.data.currentMessage.subscribe(message =>
      this.myDate = message);
    this.getFilteredTrains(this.myDate);    
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getFilteredTrains(giveDate: string) {    
    this.trainService.get(`Trains/filter-trains/${giveDate}`).subscribe((response: Train[]) =>
      this.trains = response);
  }

  newMessage() {
    this.data.getReservationDate(this.myDate);
  }
}
