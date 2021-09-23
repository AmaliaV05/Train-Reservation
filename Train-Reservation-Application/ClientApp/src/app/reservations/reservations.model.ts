import { Seat } from "../trains/train.model";

export class Reservation {
  id: number;
  code: string;
  reservationDate: string;
  seats: Seat[];
}

export class Customer {
  id: number;
  socialSecurityNumber: string;
  name: string;
  email: string;
  reservations: Reservation[];
}

export interface NewReservationRequestViewModel {
  socialSecurityNumber: string;
  name: string;
  email: string;
  reservationWithSeatsViewModel: ReservedSeatsViewModel;
}

export interface ReservedSeatsViewModel {
  reservationDate: string;
  reservedSeatsIds: number[];
}
