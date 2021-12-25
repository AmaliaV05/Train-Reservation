import { Component, ElementRef, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DataService } from '../../home/data.service';
import { CarType, DayOfWeek, TrainWithCarsViewModel } from '../train.model';
import { TrainsService } from '../trains.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html'
})
export class CarListComponent implements OnInit, OnDestroy {
  selectedTrain: TrainWithCarsViewModel = {
    id: 0,
    name: '',
    dayOfWeek: DayOfWeek.Sunday,
    cars: []
  };
  idTrain: number;
  selectedCarType = CarType.All;
  N = 1;
  reserveSeats = new Array<any>();
  reserveButton = true;
  selectedDate: Date;
  seats: number[];
  disabled: boolean;
  subscription: Subscription;

  constructor(private trainService: TrainsService,
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService) { }

  ngOnInit() {
    this.subscription = this.dataService.currentMessage$.subscribe(message =>
      this.selectedDate = message);
    this.idTrain = this.route.snapshot.params['id'];
    this.getSelectedTrain();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getSelectedTrain() {
    this.trainService.getTrainWithCars(this.idTrain, this.selectedDate, this.selectedCarType)
      .subscribe((response: TrainWithCarsViewModel) => {
        this.selectedTrain = response;
      });
  }

  getFilteredCars(item: TrainWithCarsViewModel) {
    this.selectedTrain = item;
  }

  getSelectedMultipleSeatsNumber(item: number) {
    this.N = item;
  }

  getSelectedMultipleSeatsList(item: number[]) {
    this.seats = item;
  }

  checkMultipleSeats(id: number) {
    this.disabled = !this.seats.includes(id);
    return {'background-color': this.seats.includes(id) ? 'blue' : 'white'};
  }

  addSeat(seat: any) {
    if (this.selectedCarType !== CarType.All) {
      this.reserveSeats.push(seat);
      this.reserveButton = false;
    }     
  }

  goToFinishReservation() {
    this.router.navigateByUrl('finish-reservation')
  }
}
