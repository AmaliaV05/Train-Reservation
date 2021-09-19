import { Seat } from "../trains/train.model";

export class Reservation {
  id: number;
  code: string;
  reservationDate: string;
  seat: Seat[];
}

export class Customer {
  id: number;
  socialSecurityNumber: string;
  name: string;
  email: string;
  reservation: Reservation[];
}

export interface ReservedSeats {
  reservationDate: string;
  reservedSeatsIds: number[];
}

export interface NewReservationRequest {
  socialSecurityNumber: string;
  name: string;
  email: string;
  reservedSeats: ReservedSeats;
}
