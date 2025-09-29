import { Seat } from "../../trains/graphql/types/seat-type"
import { Customer } from "./types/customer-type"

export type AddReservationPayload = {
  response: Customer;
  alternativeResponse: Seat[];
  message: string;
}

export type UpdateReservationPayload = {
  response: Customer;
  alternativeResponse: Seat[];
  message: string;
}
