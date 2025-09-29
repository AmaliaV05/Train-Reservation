import { TypedDocumentNode } from '@apollo/client/core';
import { gql } from 'apollo-angular';
import { SeatListFilterInput } from '../filters';
import { TrainQuery } from '../types/train-query';

export const GET_SEATS_BY_NUMBER: TypedDocumentNode<
  TrainQuery,
  SeatListFilterInput
  > = gql`
  query GetSeatList($filter: SeatListFilterInput!) {
    seatList(filter: $filter)
  }
`;
