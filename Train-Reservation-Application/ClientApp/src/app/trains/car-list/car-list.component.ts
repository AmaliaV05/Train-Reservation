import { Component, OnInit } from '@angular/core';
import { Car, Seat, Train, TrainWithCarsWithSeats, TYPE } from '../train-reservation.model';
import { TrainsService } from '../trains.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html'
})
export class CarListComponent implements OnInit {

  filteredCars: TrainWithCarsWithSeats[];
  TYPES = TYPE;
  idTrain: number;
  carType = 'FirstClass';

  constructor(private trainService: TrainsService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.idTrain = this.route.snapshot.params['id'];
    this.getFilteredCars();
  }

  getFilteredCars() {
    this.trainService.get(`Trains/${this.idTrain}/filter-cars/${this.carType}`)
      .subscribe((response: TrainWithCarsWithSeats[]) =>
      this.filteredCars = response);
  }
}
