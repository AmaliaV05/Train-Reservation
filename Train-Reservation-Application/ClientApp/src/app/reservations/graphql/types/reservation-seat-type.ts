import { Seat } from "../../../trains/graphql/types/seat-type";
import { Reservation } from "./reservation-type";

export type ReservationSeat = {
    reservation: Reservation;
    seat: Seat;
};
