import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Train } from '../train.model';
import { TrainsService } from '../trains.service';

@Component({
  selector: 'app-date-filter',
  templateUrl: './date-filter.component.html'
})
export class DateFilterComponent implements OnInit {

  trains: Train[];
  filterDate = new Date();
  myDate: string;

  constructor(private trainService: TrainsService,
    private router: Router) { }

  ngOnInit() {
    this.myDate = this.filterDate.toISOString().split('T')[0];
    this.getFilteredTrains(this.myDate);
  }

  getFilteredTrains(giveDate: string) {    
    this.trainService.get(`Trains/filter-trains/${giveDate}`).subscribe((response: Train[]) =>
      this.trains = response);
  }
}
