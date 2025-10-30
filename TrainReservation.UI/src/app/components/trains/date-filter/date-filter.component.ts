import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { ApolloQueryResult } from '@apollo/client/core';
import { Moment } from 'moment';
import { merge, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FeatureFlags } from '../../../core/feature-flags/feature-flags.enum';
import { getISOStringWithoutTimezone } from '../../../core/helpers/helpers';
import { ErrorService } from '../../../core/services/error.service';
import { FeatureFlagService } from '../../../core/services/feature-flag.service';
import { ValidationErrorList, ValidationErrorsHandler } from '../../validation-errors-handler';
import { DataService } from '../data.service';
import { TrainQuery } from '../graphql/types/train-query';
import { Train } from '../graphql/types/train-type';
import { TrainSearchRequest, TrainViewModel } from '../train.model';
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
  search: FormControl = new FormControl('', [Validators.required]);
  minDate: Date;
  errors: ValidationErrorList[];
  validationErrors: Record<string, string[]> = {};
  dateError: string | null = null;
  requiredMessage: string | undefined;
  matDatepickerParseMessage: string | undefined;
  matDatepickerMinMessage: string | undefined;
  hasRequiredError: boolean;
  hasMatDatepickerParseError: boolean;
  hasMatDatepickerMinError: boolean;

  constructor(private trainService: TrainsService,
    private trainGraphqlService: TrainsGraphqlService,
    private featureFlagService: FeatureFlagService,
    private dataService: DataService,
    private validationErrordHandler: ValidationErrorsHandler,
    private errorService: ErrorService,
    private _adapter: DateAdapter<Date>) {
    this._adapter.setLocale('ro');
    this.minDate = new Date();
  }

  ngOnInit() {    
    this.subscription = this.dataService.currentMessage$.subscribe(message =>
      this.selectedDate = message);

    this.getFormValidationErrorMessages();

    // TODO: Integrate server-side validation responses into form controls.
    // This should map backend validation errors to Angular FormGroup errors.
    /*this.getServerFormValidationErrorMessages();*/
  } 

  getTrainList() {
    this.selectedValue = this.search.value as Moment;
    this.selectedDate = this.selectedValue.toDate();

    if (this.featureFlagService.isEnabled(FeatureFlags.UseGraphQL)) {
      let selectedDay = this.selectedDate.getDay();

      this.trainGraphqlService.getTrainsByDate(selectedDay).subscribe((response: ApolloQueryResult<TrainQuery>) => {
        this.trains = response.data?.trainsByDate;
      });
    }
    else {
      let request: TrainSearchRequest = {
        date: getISOStringWithoutTimezone(this.selectedDate)
      };

      this.trainService.getTrainList(request).subscribe((response: TrainViewModel[]) =>
        this.trains = response);      
    }

    this.dataService.getReservationDate(this.selectedDate);
  } 

  private getFormValidationErrorMessages(): void {
    const value$ = this.search.valueChanges.pipe(debounceTime(300));
    const status$ = this.search.statusChanges;
    
    this.subscription = merge(value$, status$).subscribe(() => {
      this.errors = this.validationErrordHandler.parseFormErrors(this.search, 'search');
      if (this.errors?.length) {
        this.setValidationMessages();
      } else {
        this.resetValidationMessages();
      }
    });
  }

  private setValidationMessages(): void {
    this.hasRequiredError = this.search.hasError('required') && !this.search.hasError('matDatepickerParse');
    this.hasMatDatepickerParseError = this.search.hasError('required') && this.search.hasError('matDatepickerParse');
    this.hasMatDatepickerMinError = this.search.hasError('matDatepickerMin');

    this.requiredMessage = this.validationErrordHandler.getErrorMessage(this.errors, 'search', 'required');
    this.matDatepickerParseMessage = this.validationErrordHandler.getErrorMessage(this.errors, 'search', 'matDatepickerParse');
    this.matDatepickerMinMessage = this.validationErrordHandler.getErrorMessage(this.errors, 'search', 'matDatepickerMin');    
  }

  private resetValidationMessages(): void {
    this.hasRequiredError = false;
    this.hasMatDatepickerParseError = false;
    this.hasMatDatepickerMinError = false;

    this.requiredMessage = undefined;
    this.matDatepickerParseMessage = undefined;
    this.matDatepickerMinMessage = undefined;
  }

  private getServerFormValidationErrorMessages(): void {
    this.subscription = this.search.valueChanges.pipe(debounceTime(500), distinctUntilChanged()).subscribe(() => {
      if (this.search?.hasError('serverError')) {
        // Keep client-side errors but remove server-side ones
        const errors = { ...this.search.errors };
        delete errors['serverError'];
        this.search.setErrors(Object.keys(errors).length ? errors : null);
      }

      this.errorService.error$.subscribe(() => {
        this.validationErrors = this.errorService.getValidationErrors();
        if (this.validationErrors !== {}) {
          this.search.setErrors({ serverError: this.validationErrors['date'][0] });
          this.dateError = this.search.getError('serverError');
        }
      });
    });
  }

  private getFieldErrors(field: string): string[] {
   const errors = this.errorService.getValidationErrors();
    return errors[field] || [];
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
