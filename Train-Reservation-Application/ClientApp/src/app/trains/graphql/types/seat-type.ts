import { Car } from "./car-type";
import { SeatCalendar } from "./seat-calendar-type";
import { Calendar } from "./calendar-type";
import { Reservation } from "../../../reservations/graphql/types/reservation-type";
import { ReservationSeat } from "../../../reservations/graphql/types/reservation-seat-type";


export type Seat = {
    id: number;
    number: number;
    car: Car;
    reservations: Reservation[];
    calendars: Calendar[];
    seatCalendars: SeatCalendar[];
    reservationSeats: ReservationSeat[];
};
