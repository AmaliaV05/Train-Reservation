import { Reservation } from "../reservations/reservations.model";

export class Train {
  id: number;
  name: string;
  dayOfWeek: string;
  cars: Car[];
}

export enum Type {
  All,
  FirstClass,
  SecondClass,
  Sleeping
}

export class Car {
  id: number;
  carNumber: number;
  numberOfSeats: number;
  type: Type;
  seats: Seat[];
}

export class Seat {
  id: number;
  number: number;
  reservations: Reservation[];
  calendars: Calendar[];
  seatCalendars: SeatCalendar[];
}

export class SeatCalendar {
  seat: Seat;
  calendar: Calendar;
  availability: string; 
}

export class Calendar {
  id: number;
  calendarDate: string;
  seats: Seat[];
  seatCalendars: SeatCalendar[];
}

export interface TrainWithCarsViewModel {
  id: number;
  name: string;
  dayOfWeek: string;
  cars: CarWithSeatsViewModel[];
}

export interface CarWithSeatsViewModel {
  id: number;
  carNumber: number;
  numberOfSeats: number;
  type: Type;
  seats: SeatViewModel[];
}

export interface SeatViewModel {
  id: number;
  number: number;
  seatCalendars: SeatCalendarViewModel[];
}

export interface SeatCalendarViewModel {
  seatAvailability: string;
}
