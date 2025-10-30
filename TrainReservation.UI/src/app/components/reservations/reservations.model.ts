import { CarType } from "../trains/enums";
import { TrainViewModel } from "../trains/train.model";

export class ResponseService<TResponse, TAlternativeResponse, TMessage>
{
  response: TResponse;
  alternativeResponse: TAlternativeResponse;
  message: TMessage;
}

export interface TicketViewModel {
  name: string;
  reservations: ReservationViewModel[];
}

export interface ReservationViewModel {
  id: number;
  code: string;
  reservationDate: Date;
  seats: SeatInCarViewModel[];
}

export interface SeatInCarViewModel {
  id: number;
  number: number;
  car: CarInTrainViewModel;
}

export interface CarInTrainViewModel {
  id: number;
  carNumber: number;
  type: CarType;
  train: TrainViewModel;
}

export interface NewReservationRequest {
  socialSecurityNumber: string;
  name: string;
  email: string;
  reservationWithSeatsViewModel: ReservedSeatsViewModel;
}

export interface ReservedSeatsViewModel {
  reservationDate: Date;
  reservedSeatsIds: number[];
}

export interface ModifyReservationViewModel {
  id: number;
  code: string;
  reservationDate: Date;
  reservedSeatsIds: number[];
}
