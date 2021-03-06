import { Component, OnDestroy, OnInit } from '@angular/core';
import { TrainViewModel } from '../train.model';
import { TrainsService } from '../trains.service';
import { Subscription } from 'rxjs';
import { DateAdapter } from '@angular/material/core';
import { FormControl } from '@angular/forms';
import { DataService } from '../data.service';

@Component({
  selector: 'app-date-filter',
  templateUrl: './date-filter.component.html'
})
export class DateFilterComponent implements OnInit, OnDestroy {

  trains: TrainViewModel[];
  selectedDate: Date;
  subscription: Subscription;
  search = new FormControl('');
  minDate: Date;

  constructor(private trainService: TrainsService,
    private dataService: DataService,
    private _adapter: DateAdapter<Date>) {
    this._adapter.setLocale('ro');
    this.minDate = new Date();
  }

  ngOnInit() {    
    this.subscription = this.dataService.currentMessage$.subscribe(message =>
      this.selectedDate = message);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getTrainList() {
    this.selectedDate = this.search.value.toISOString().split('T')[0];
    this.trainService.getTrainList(this.selectedDate).subscribe((response: TrainViewModel[]) =>
      this.trains = response);
    this.dataService.getReservationDate(this.selectedDate);
  }
}
