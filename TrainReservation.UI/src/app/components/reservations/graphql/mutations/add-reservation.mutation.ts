import { TypedDocumentNode } from '@apollo/client/core';
import { gql } from 'apollo-angular';
import { AddReservationInput } from '../inputs';
import { ReservationMutation } from '../types/reservation-mutation';

export const ADD_RESERVATION: TypedDocumentNode<
  ReservationMutation,
  AddReservationInput
  > = gql`
mutation NewReservation($input: AddReservationInput!) {
  addReservation(input: $input) {
    message
    response {
      name
      reservations {
        id
        code
        reservationDate
        seats {
          id
          number
          car {
            id
            carNumber
            type
            train {
              id
              name
            }
          }
        }
      }
    }
    alternativeResponse {
      id
      number
      car {
        id
        carNumber
        type
        train {
          id
          name
        }
      }
    }
  }
}
`;
