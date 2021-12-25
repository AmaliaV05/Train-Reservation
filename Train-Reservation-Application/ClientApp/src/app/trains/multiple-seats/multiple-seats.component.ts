import { Component, EventEmitter, Input, Output } from "@angular/core";
import { TrainsService } from "../trains.service";

interface SelectGroupSeats {
  value: number;
  text: string;
}

@Component({
  selector: 'app-multiple-seats',
  templateUrl: './multiple-seats.component.html'
})

export class MultipleSeatsComponent {
  availableGroupSeatIds: number[];
  N: number;

  @Input() idTrain: number;
  @Input() selectedDate: Date;
  @Output() selectedGroupSeatsNumber = new EventEmitter<number>();
  @Output() selectedGroupSeatsList = new EventEmitter<number[]>();

  constructor(private trainService: TrainsService) { }

  multipleSeats: SelectGroupSeats[] = [
    { value: 1, text: '1' },
    { value: 2, text: '2' },
    { value: 3, text: '3' },
    { value: 4, text: '4' },
    { value: 5, text: '5' },
    { value: 6, text: '6' },
    { value: 7, text: '7' }
  ];

  onChangeMultipleSeatsNumber(option) {
    this.N = option.value;
    this.trainService.getTrainWithMultipleAvailableSeats(this.idTrain, this.selectedDate, this.N)
      .subscribe((response: number[]) => {
        this.availableGroupSeatIds = response;
        this.selectedGroupSeatsNumber.emit(this.N);
        this.selectedGroupSeatsList.emit(this.availableGroupSeatIds);
      });
  }
}
