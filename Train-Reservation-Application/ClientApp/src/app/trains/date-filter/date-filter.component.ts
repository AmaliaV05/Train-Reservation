import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { ApolloQueryResult } from '@apollo/client/core';
import { Moment } from 'moment';
import { Subscription } from 'rxjs';
import { FeatureFlagService } from '../../core/feature-flag.service';
import { FeatureFlags } from '../../core/feature-flags/feature-flags.enum';
import { getISOStringWithoutTimezone } from '../../core/helpers/helpers';
import { DataService } from '../data.service';
import { TrainQuery } from '../graphql/types/train-query';
import { Train } from '../graphql/types/train-type';
import { TrainViewModel } from '../train.model';
import { TrainsGraphqlService } from '../trains-graphql.service';
import { TrainsService } from '../trains.service';

@Component({
  selector: 'app-date-filter',
  templateUrl: './date-filter.component.html'
})
export class DateFilterComponent implements OnInit, OnDestroy {
  trains: TrainViewModel[] | Train[];
  selectedValue: Moment;
  selectedDate: Date;
  subscription: Subscription;
  search = new FormControl('');
  minDate: Date;

  constructor(private trainService: TrainsService,
    private trainGraphqlService: TrainsGraphqlService,
    private featureFlagService: FeatureFlagService,
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
    this.selectedValue = this.search.value as Moment;
    this.selectedDate = this.selectedValue.toDate();

    if (this.featureFlagService.isEnabled(FeatureFlags.UseGraphQL)) {
      let selectedDate = this.selectedValue.toDate();
      let selectedDay = selectedDate.getDay();

      this.trainGraphqlService.getTrainsByDate(selectedDay).subscribe((response: ApolloQueryResult<TrainQuery>) => {
        this.trains = response.data?.trainsByDate;
      });
    }
    else {
      let selectedDate = getISOStringWithoutTimezone(this.selectedDate);

      this.trainService.getTrainList(selectedDate).subscribe((response: TrainViewModel[]) =>
        this.trains = response);
    }

    this.dataService.getReservationDate(this.selectedDate);
  }
}
