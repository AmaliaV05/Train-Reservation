import { Component, OnDestroy, OnInit } from '@angular/core';
import { Seat, TrainWithCarsViewModel } from '../train.model';
import { TrainsService } from '../trains.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../../home/data.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html'
})
export class CarListComponent implements OnInit, OnDestroy {

  filteredCars: TrainWithCarsViewModel[];
  idTrain: number;
  selectedCarType = 'All';
  carTypes = [
    { value: 'FirstClass', text: 'First Class' },
    { value: 'SecondClass', text: 'Second Class' },
    { value: 'Sleeping', text: 'Sleeping' }
  ];
  reserveSeats = new Array<Seat>();
  filteredCarsByN: TrainWithCarsViewModel[];
  N = 1;
  addButtonDisabled = false;
  removeButtonDisabled = true;
  reserveButton = true;
  filterDate = new Date();
  myDate: string;// = '2021-09-14';
  subscription: Subscription;

  constructor(private trainService: TrainsService,
    private route: ActivatedRoute,
    private router: Router,
    private data: DataService) { }

  ngOnInit() {
    this.subscription = this.data.currentMessage.subscribe(message =>
      this.myDate = message);
    this.idTrain = this.route.snapshot.params['id'];    
    this.getFilteredCars();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getFilteredCars() {
    //this.myDate = this.filterDate.toISOString().split('T')[0];
    this.trainService.get(`Trains/${this.idTrain}/${this.myDate}/filter-cars/${this.selectedCarType}`)
      .subscribe((response: TrainWithCarsViewModel[]) =>
        this.filteredCars = response);
  }

  onChangeFilteredCars(option) {
    let selectedCarType = option.value;
    //this.myDate = this.filterDate.toISOString().split('T')[0];
    this.trainService.get(`Trains/${this.idTrain}/${this.myDate}/filter-cars/${selectedCarType}`)
      .subscribe((response: TrainWithCarsViewModel[]) =>
        this.filteredCars = response);
  }

  addSeat(seat: Seat) {
    if (this.selectedCarType !== 'All') {
      this.reserveSeats.push(seat);
      this.reserveButton = false;
    }     
  }

  incrementSeatNumber() {
    this.N++;
    this.addButtonDisabled = this.N < 8 ? false : true;
    this.removeButtonDisabled = this.N > 1 ? false : true;
    this.trainService.get(`Trains/${this.idTrain}/filter-cars-by-available-seats/${this.N}`)
      .subscribe((response: TrainWithCarsViewModel[]) => {
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
      .subscribe((response: TrainWithCarsViewModel[]) => {
        this.filteredCarsByN = response;
        console.log(response);
      }
      );
  }

  goToFinishReservation() {
    this.router.navigateByUrl('finish-reservation')
  }

  getCurrentReservationDate() {
    this.data.getReservationDate(this.myDate);    
  }
}
