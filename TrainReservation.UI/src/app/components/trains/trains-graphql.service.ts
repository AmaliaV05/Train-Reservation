import { Injectable } from '@angular/core';
import { ApolloQueryResult } from '@apollo/client/core';
import { Apollo } from 'apollo-angular';
import { Observable } from 'rxjs';
import { CarType, DayOfWeek } from './enums';
import { CarTypeFilterInput, DateFilterInput, SeatListFilterInput } from './graphql/filters';
import { GET_CARS_BY_TYPE } from './graphql/queries/car-list.query';
import { GET_TRAINS_BY_DATE } from './graphql/queries/date-filter.query';
import { GET_SEATS_BY_NUMBER } from './graphql/queries/seat-list.query';
import { TrainQuery } from './graphql/types/train-query';

@Injectable({
  providedIn: 'root'
})
export class TrainsGraphqlService {
  constructor(private apollo: Apollo) { }

  getTrainsByDate(selectedDay: DayOfWeek): Observable<ApolloQueryResult<TrainQuery>> {
    return this.apollo.query<TrainQuery, DateFilterInput>({
      query: GET_TRAINS_BY_DATE,
      variables: {
        filter: {
          dayOfWeek: DayOfWeek[selectedDay].toString()
        }
      }
    });
  }

  getCarsByType(trainId: number, selectedDate: Date, selectedCarType: CarType): Observable<ApolloQueryResult<TrainQuery>> {
    return this.apollo.query<TrainQuery, CarTypeFilterInput>({
      query: GET_CARS_BY_TYPE,
      variables: {
        filter: {
          id: trainId,
          calendarDate: selectedDate.toISOString(),
          type: CarType[selectedCarType]
        }        
      }
    });
  }

  getSeatList(trainId: number, selectedDate: Date, numberOfSeats: number): Observable<ApolloQueryResult<TrainQuery>> {
    return this.apollo.query<TrainQuery, SeatListFilterInput>({
      query: GET_SEATS_BY_NUMBER,
      variables: {
        filter: {
          id: trainId,
          calendarDate: selectedDate.toISOString(),
          n: numberOfSeats
        }        
      }
    });
  }
}
