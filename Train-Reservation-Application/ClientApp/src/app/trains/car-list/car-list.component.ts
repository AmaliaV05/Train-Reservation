import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApolloQueryResult } from '@apollo/client/core';
import { Subscription } from 'rxjs';
import { FeatureFlagService } from '../../core/feature-flag.service';
import { FeatureFlags } from '../../core/feature-flags/feature-flags.enum';
import { getISOStringWithoutTimezone } from '../../core/helpers/helpers';
import { DataService } from '../data.service';
import { CarType } from '../enums';
import { TrainQuery } from '../graphql/types/train-query';
import { Train } from '../graphql/types/train-type';
import { CarWithSeatsViewModel, SeatViewModel, TrainWithCarsViewModel } from '../train.model';
import { TrainsGraphqlService } from '../trains-graphql.service';
import { TrainsService } from '../trains.service';

class SelectedSeatsView {
  car: number;
  seat: number;
}

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html'
})
export class CarListComponent implements OnInit, OnDestroy {
  selectedTrain: TrainWithCarsViewModel | Train = {
    id: 0,
    name: '',
    cars: [{
      id: 0,
      carNumber: 0,
      numberOfSeats: 0,
      type: CarType.ALL,
      seats: [{
        id: 0,
        number: 0,
        seatCalendars: [{
          seatAvailability: false
        }]
      }]
    }]
  };
  idTrain: number;
  selectedCarType = CarType.ALL;
  N = 1;
  selectedDate: Date;
  seats: number[];
  disabled: boolean;
  reserveSeatsView = new Array<SelectedSeatsView>();
  reserveSeatView: SelectedSeatsView = { car: 0, seat: 0 };
  reserveSeatsIds = new Array<number>();
  reserveButton = true;
  message: string;
  subscription: Subscription;
  seatsListSubscription: Subscription;

  constructor(private trainService: TrainsService,
    private trainGraphqlService: TrainsGraphqlService,
    private featureFlagService: FeatureFlagService,
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService) { }

  ngOnInit() {
    this.subscription = this.dataService.currentMessage$.subscribe(message =>
      this.selectedDate = message);
    this.seatsListSubscription = this.dataService.currentSeatListMessage$.subscribe(message =>
      this.reserveSeatsIds = message);
    this.idTrain = parseInt(this.route.snapshot.params['id']);
    this.getSelectedTrain();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    this.seatsListSubscription.unsubscribe();
  }

  getSelectedTrain() {
    if (this.featureFlagService.isEnabled(FeatureFlags.UseGraphQL)) {
      this.trainGraphqlService.getCarsByType(this.idTrain, this.selectedDate, this.selectedCarType)
        .subscribe((response: ApolloQueryResult<TrainQuery>) => {
          this.selectedTrain = response.data?.carsByType;
        });
    }
    else {
      let selectedDate = getISOStringWithoutTimezone(this.selectedDate);

      this.trainService.getTrainWithCars(this.idTrain, selectedDate, this.selectedCarType)
        .subscribe((response: TrainWithCarsViewModel) => {
          this.selectedTrain = response;
        });
    }    
  }

  getFilteredCars(item: TrainWithCarsViewModel) {
    this.selectedTrain = item;
  }

  getSelectedCarType(item: CarType) {
    this.selectedCarType = item;
  }

  getSelectedMultipleSeatsNumber(item: number) {
    this.N = item;
  }

  getSelectedMultipleSeatsList(item: number[]) {
    this.seats = item;
  }

  checkMultipleSeats(id: number) {
    this.disabled = !this.seats.includes(id);
    return { 'background-color': this.seats.includes(id) ? '#00E828' : 'red'};
  }

  addSeat(car: CarWithSeatsViewModel, seat: SeatViewModel) {
    if (this.selectedCarType != CarType.ALL && !this.reserveSeatsIds.includes(seat.id)) {
      this.reserveSeatView.car = car.carNumber;
      this.reserveSeatView.seat = seat.number;
      this.reserveSeatsView.push(this.reserveSeatView);
      this.reserveSeatView = { car: 0, seat: 0 };
      this.reserveSeatsIds.push(seat.id);
      this.reserveButton = false;
    }
    else
      if (this.reserveSeatsIds.includes(seat.id)) {
        for (var i = 0; i < this.reserveSeatsIds.length; i++) {
          if (this.reserveSeatsIds[i] === seat.id) {
            this.reserveSeatsIds.splice(i, 1);
            this.reserveSeatsView.splice(i, 1);
            break;
          }
        }
      }
  }

  goToFinishReservation() {
    if (this.reserveSeatsView.length === 0) {
      this.message = 'Please select at least one seat!';
    }
    else {
      this.dataService.getSeatsIdsList(this.reserveSeatsIds);
      this.router.navigateByUrl('finish-reservation');
    }    
  }
}
