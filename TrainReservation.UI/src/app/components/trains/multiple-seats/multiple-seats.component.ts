import { Component, EventEmitter, Input, Output } from "@angular/core";
import { MatSelectChange } from "@angular/material/select";
import { ApolloQueryResult } from "@apollo/client/core";
import { FeatureFlags } from "../../../core/feature-flags/feature-flags.enum";
import { FeatureFlagService } from "../../../core/services/feature-flag.service";
import { getISOStringWithoutTimezone } from "../../../core/helpers/helpers";
import { TrainQuery } from "../graphql/types/train-query";
import { TrainsGraphqlService } from "../trains-graphql.service";
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

  constructor(private trainService: TrainsService,
    private trainGraphqlService: TrainsGraphqlService,
    private featureFlagService: FeatureFlagService) { }

  multipleSeats: SelectGroupSeats[] = [
    { value: 1, text: '1' },
    { value: 2, text: '2' },
    { value: 3, text: '3' },
    { value: 4, text: '4' },
    { value: 5, text: '5' },
    { value: 6, text: '6' },
    { value: 7, text: '7' }
  ];

  onChangeMultipleSeatsNumber(event: MatSelectChange) {
    this.N = event.value as number;

    if (this.featureFlagService.isEnabled(FeatureFlags.UseGraphQL)) {
      this.trainGraphqlService.getSeatList(this.idTrain, this.selectedDate, this.N).subscribe((response: ApolloQueryResult<TrainQuery>) => {
        this.availableGroupSeatIds = response.data?.seatList;
        this.selectedGroupSeatsNumber.emit(this.N);
        this.selectedGroupSeatsList.emit(this.availableGroupSeatIds);
      });
    }
    else {
      let selectedDate = getISOStringWithoutTimezone(this.selectedDate);

      this.trainService.getTrainWithMultipleAvailableSeats(this.idTrain, selectedDate, this.N)
        .subscribe((response: number[]) => {
          this.availableGroupSeatIds = response;
          this.selectedGroupSeatsNumber.emit(this.N);
          this.selectedGroupSeatsList.emit(this.availableGroupSeatIds);
        });
    }    
  }
}
