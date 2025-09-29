import { Calendar } from "./calendar-type";
import { Seat } from "./seat-type";


export type SeatCalendar = {
    seat: Seat;
    calendar: Calendar;
    seatAvailability: boolean;
};
