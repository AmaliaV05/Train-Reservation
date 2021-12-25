export enum CarType {
  All,
  FirstClass,
  SecondClass,
  Sleeping
}

export enum DayOfWeek {
  Sunday,
  Monday,
  Tuesday,
  Wednesday,
  Thursday,
  Friday,
  Saturday
}

export interface TrainViewModel {
  id: number;
  name: string;
  dayOfWeek: DayOfWeek;
}

export interface TrainWithCarsViewModel {
  id: number;
  name: string;
  dayOfWeek: DayOfWeek;
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
