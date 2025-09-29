import { Component, EventEmitter, Input, Output } from "@angular/core";
import { ApolloQueryResult } from "@apollo/client/core";
import { FeatureFlags } from "../../core/feature-flags/feature-flags.enum";
import { FeatureFlagService } from "../../core/feature-flag.service";
import { getISOStringWithoutTimezone } from "../../core/helpers/helpers";
import { CarType } from "../enums";
import { TrainQuery } from "../graphql/types/train-query";
import { Train } from "../graphql/types/train-type";
import { TrainWithCarsViewModel } from "../train.model";
import { TrainsGraphqlService } from "../trains-graphql.service";
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
  filteredCars: TrainWithCarsViewModel | Train;
  carTypes: SelectCarType[] = [
    { value: CarType.FIRST_CLASS, text: 'First Class' },
    { value: CarType.SECOND_CLASS, text: 'Second Class' },
    { value: CarType.SLEEPING, text: 'Sleeping' }
  ];
  selectedCarType: CarType;

  @Input() idTrain: number;
  @Input() selectedDate: Date;
  @Output() filteredCarsByType = new EventEmitter<TrainWithCarsViewModel | Train>();
  @Output() selectCarType = new EventEmitter<CarType>();

  constructor(private trainService: TrainsService,
    private trainGraphqlService: TrainsGraphqlService,
    private featureFlagService: FeatureFlagService) { }

  onChangeFilteredCars(option) {
    this.selectedCarType = option.value as CarType;

    if (this.featureFlagService.isEnabled(FeatureFlags.UseGraphQL)) {
      this.trainGraphqlService.getCarsByType(this.idTrain, this.selectedDate, this.selectedCarType)
        .subscribe((response: ApolloQueryResult<TrainQuery>) => {
          this.filteredCars = response.data?.carsByType;
          this.filteredCarsByType.emit(this.filteredCars);
          this.selectCarType.emit(this.selectedCarType);
        });
    }
    else {
      let selectedDate = getISOStringWithoutTimezone(this.selectedDate);

      this.trainService.getTrainWithCars(this.idTrain, selectedDate, this.selectedCarType)
        .subscribe((response: TrainWithCarsViewModel) => {
          this.filteredCars = response;
          this.filteredCarsByType.emit(this.filteredCars);
          this.selectCarType.emit(this.selectedCarType);
        });
    }
  }
}
