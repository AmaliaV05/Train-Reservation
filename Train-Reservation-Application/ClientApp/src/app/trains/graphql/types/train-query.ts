import { Train } from "./train-type";

export type TrainQuery = {
  trainsByDate: Train[];
  carsByType: Train;
  seatList: number[];
};
