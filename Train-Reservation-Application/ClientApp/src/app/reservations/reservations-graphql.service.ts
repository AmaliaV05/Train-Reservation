import { Injectable } from '@angular/core';
import { FetchResult } from '@apollo/client/core';
import { Apollo } from 'apollo-angular';
import { Observable } from 'rxjs';
import { AddReservationInput, UpdateReservationInput } from './graphql/inputs';
import { ADD_RESERVATION } from './graphql/mutations/add-reservation.mutation';
import { UPDATE_RESERVATION } from './graphql/mutations/update-reservation.mutation';
import { ReservationMutation } from './graphql/types/reservation-mutation';

@Injectable({
  providedIn: 'root'
})
export class ReservationsGraphqlService {
  constructor(private apollo: Apollo) { }

  makeReservation(
    socialSecurityNumber: string,
    name: string,
    email: string,
    reservationDate: Date,
    reservedSeatsIds: number[]): Observable<FetchResult<ReservationMutation>> {
    return this.apollo.mutate<ReservationMutation, AddReservationInput>({
      mutation: ADD_RESERVATION,
      variables: {
        input: {
          socialSecurityNumber: socialSecurityNumber,
          name: name,
          email: email,
          reservationDate: reservationDate.toISOString(),
          reservedSeatsIds: reservedSeatsIds
        }        
      }
    });
  }

  modifyReservation(
    idReservation: number,
    id: number,
    code: string,
    reservationDate: Date,
    reservedSeatsIds: number[]): Observable<FetchResult<ReservationMutation>> {
    return this.apollo.mutate<ReservationMutation, UpdateReservationInput>({
      mutation: UPDATE_RESERVATION,
      variables: {
        input: {
          idReservation: idReservation,
          reservation: {
            id: id,
            code: code,
            reservationDate: reservationDate.toISOString(),
            reservedSeatsIds: reservedSeatsIds
          }
        }
      }
    });
  }
}
