import { Seat } from "./seat-type";
import { SeatCalendar } from "./seat-calendar-type";

export type Calendar = {
    id: number;
    calendarDate: Date;
    seats: Seat[];
    seatCalendars: SeatCalendar[];
};
