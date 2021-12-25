import { Component, EventEmitter, Input, Output } from "@angular/core";
import { CarType, TrainWithCarsViewModel } from "../train.model";
import { TrainsService } from "../trains.service";

interface SelectCarType {
  value: CarType;
  text: string;
}

@Component({
  selector: 'app-select-car',
  templateUrl: './select-car.component.html'
})

export class SelectCarComponent {
  filteredCars: TrainWithCarsViewModel;
  carTypes: SelectCarType[] = [
    { value: CarType.FirstClass, text: 'First Class' },
    { value: CarType.SecondClass, text: 'Second Class' },
    { value: CarType.Sleeping, text: 'Sleeping' }
  ];
  selectedCarType: CarType;

  @Input() idTrain: number;
  @Input() selectedDate: Date;
  @Output() filteredCarsByType = new EventEmitter<TrainWithCarsViewModel>();

  constructor(private trainService: TrainsService) { }

  onChangeFilteredCars(option) {
    this.selectedCarType = option.value;
    this.trainService.getTrainWithCars(this.idTrain, this.selectedDate, this.selectedCarType)
      .subscribe((response: TrainWithCarsViewModel) => {
        this.filteredCars = response;
        this.filteredCarsByType.emit(this.filteredCars);
      });
  }
}
