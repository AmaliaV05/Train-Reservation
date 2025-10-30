import { TypedDocumentNode } from '@apollo/client/core';
import { gql } from 'apollo-angular';
import { UpdateReservationInput } from '../inputs';
import { ReservationMutation } from '../types/reservation-mutation';

export const UPDATE_RESERVATION: TypedDocumentNode<
  ReservationMutation,
  UpdateReservationInput
> = gql`
mutation UpdateReservation($input: UpdateReservationInput!) {
  updateReservation(input: $input) {
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
