import { TypedDocumentNode } from '@apollo/client/core';
import { gql } from 'apollo-angular';
import { DateFilterInput } from '../filters';
import { TrainQuery } from '../types/train-query';

export const GET_TRAINS_BY_DATE: TypedDocumentNode<
  TrainQuery,
  DateFilterInput
  > = gql`
  query FilterTrainsByDate($filter: DateFilterInput!) {
   trainsByDate(filter: $filter) {
     id
     name
   }
 }
`;
