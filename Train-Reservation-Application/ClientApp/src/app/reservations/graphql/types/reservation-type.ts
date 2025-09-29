import { Seat } from "../../../trains/graphql/types/seat-type";
import { Customer } from "./customer-type";
import { ReservationSeat } from "./reservation-seat-type";

export type Reservation = {
    id: number;
    code: string;
    reservationDate: Date;
    phoneNumber: string;
    seats: Seat[];
    customer: Customer;
    reservationSeats: ReservationSeat[];
};
