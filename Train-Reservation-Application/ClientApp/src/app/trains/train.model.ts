import { CarType } from "./enums";

export interface TrainViewModel {
  id: number;
  name: string;
}

export interface TrainWithCarsViewModel {
  id: number;
  name: string;
  cars: CarWithSeatsViewModel[];
}

export interface CarWithSeatsViewModel {
  id: number;
  carNumber: number;
  numberOfSeats: number;
  type: CarType;
  seats: SeatViewModel[];
}

export interface SeatViewModel {
  id: number;
  number: number;
  seatCalendars: SeatCalendarViewModel[];
}

export interface SeatCalendarViewModel {
  seatAvailability: boolean;
}
