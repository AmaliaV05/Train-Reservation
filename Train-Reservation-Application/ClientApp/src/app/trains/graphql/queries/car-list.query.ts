import { TypedDocumentNode } from '@apollo/client/core';
import { gql } from 'apollo-angular';
import { CarTypeFilterInput } from '../filters';
import { TrainQuery } from '../types/train-query';

export const GET_CARS_BY_TYPE: TypedDocumentNode<
  TrainQuery,
  CarTypeFilterInput
  > = gql`
  query FilterCarsByType($filter: CarTypeFilterInput!) {
    carsByType(filter: $filter) {
      id
      name
      cars {
        carNumber
        type
        seats {
          id
          number
          seatCalendars {
            seatAvailability
          }
        }
      }
    }
  }
`;
