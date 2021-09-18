export class Train {
  id: number;
  name: string;
  dayOfWeek: string;
  cars: Car[];
}

export enum Type {
  All = 0,
  FirstClass = 1,
  SecondClass = 2,
  Sleeping = 3
}

export const TYPE = ['All', 'First Class', 'Second Class', 'Sleeping'];

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
  available: string;
}

export interface TrainWithCarsWithSeats {
  id: number;
  name: string;
  dayOfWeek: string;
  cars: CarsWithSeats[];
}

export interface CarsWithSeats {
  id: number;
  carNumber: number;
  numberOfSeats: number;
  type: Type;
  seats: Seat[];
}
