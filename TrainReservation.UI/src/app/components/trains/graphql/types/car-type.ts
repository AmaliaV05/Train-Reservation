import { CarType } from "../../enums";
import { Seat } from "./seat-type";
import { Train } from "./train-type";

export type Car = {
    id: string;
    carNumber: number;
    numberOfSeats: number;
    type: CarType;
    train: Train;
    seats: Seat[];
};
