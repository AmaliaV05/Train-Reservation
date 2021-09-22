import { Component, Input, OnInit } from '@angular/core';
import { Car, CarsWithSeats, Seat, Train, TrainWithCarsWithSeats, TYPE } from '../train.model';
import { TrainsService } from '../trains.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html'
})
export class CarListComponent implements OnInit {

  filteredCars: TrainWithCarsWithSeats[];
  TYPES = TYPE;
  idTrain: number;
  carType: string;
  reserveSeats = new Array<Seat>();
  filteredCarsByN: TrainWithCarsWithSeats[];
  N = 1;
  addButtonDisabled = false;
  removeButtonDisabled = true;
  reserveButton = true;
  filterDate = new Date();
  myDate: string;
  flatArray: TrainWithCarsWithSeats[];

  constructor(private trainService: TrainsService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.myDate = this.filterDate.toISOString().split('T')[0];
    this.idTrain = this.route.snapshot.params['id'];
    this.eventSelection(event);
    this.getFilteredCars(this.myDate);   
  }

  getFilteredCars(giveDate: string) {
    this.trainService.get(`Trains/${this.idTrain}/${giveDate}/filter-cars/${this.carType}`)
      .subscribe((response: TrainWithCarsWithSeats[]) =>
        this.filteredCars = response);
  }

  addSeat(seat: Seat) {
    this.reserveSeats.push(seat);
    this.reserveButton = false; 
  }

  incrementSeatNumber() {
    this.N++;
    this.addButtonDisabled = this.N < 8 ? false : true;
    this.removeButtonDisabled = this.N > 1 ? false : true;
    this.trainService.get(`Trains/${this.idTrain}/filter-cars-by-available-seats/${this.N}`)
      .subscribe((response: TrainWithCarsWithSeats[]) => {
        this.filteredCarsByN = response;
        console.log(response);
      }
      );
  }

  decrementSeatNumber() {
    this.addButtonDisabled = this.N >= 1 ? false : true;
    this.N--;
    this.removeButtonDisabled = this.N === 1 ? true : false;
    this.trainService.get(`Trains/${this.idTrain}/filter-cars-by-available-seats/${this.N}`)
      .subscribe((response: TrainWithCarsWithSeats[]) => {
        this.filteredCarsByN = response;
        console.log(response);
      }
      );
  }

  goToFinishReservation() {
    this.router.navigateByUrl('finish-reservation')
  }

  eventSelection(event) {
    this.carType = event;
  }
}
