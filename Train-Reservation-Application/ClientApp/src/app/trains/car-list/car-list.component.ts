import { Component, OnInit } from '@angular/core';
import { Car, CarsWithSeats, Seat, Train, TrainWithCarsWithSeats, TYPE } from '../train.model';
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
  carType = 'All';
  reserveSeats = new Array<Seat>();
  filteredCarsByN: TrainWithCarsWithSeats[];
  N = 1;

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

  getGroupSeats() {
    this.trainService.get(`Trains/${this.idTrain}/filter-cars-by-available-seats/${this.N}`)
      .subscribe((response: TrainWithCarsWithSeats[]) => {
        this.filteredCarsByN = response;
        console.log(response);}
        );
  }

  addSeat(seat: Seat) {
    this.reserveSeats.push(seat);
  }
}
