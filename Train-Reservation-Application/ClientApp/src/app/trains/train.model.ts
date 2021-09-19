export class Train {
  id: number;
  name: string;
  dayOfWeek: string;
  cars: Car[];
}

export enum Type {
  All = "All",
  FirstClass = "First Class",
  SecondClass = "Second Class",
  Sleeping = "Slepping"
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
