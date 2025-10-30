import { AddReservationPayload, UpdateReservationPayload } from "../payloads";

export type ReservationMutation = {
  addReservation: AddReservationPayload;
  updateReservation: UpdateReservationPayload;
};
