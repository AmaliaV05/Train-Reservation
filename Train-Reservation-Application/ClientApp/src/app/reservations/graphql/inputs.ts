export type AddReservationInput = {
  input: {
    socialSecurityNumber: string;
    name: string;
    email: string;
    reservationDate: string;
    reservedSeatsIds: number[];
  }  
};

export type UpdateReservationInput = {
  input: {
    idReservation: number;
    reservation: UpdateReservationDetailsInput
  }
};

export type UpdateReservationDetailsInput = {
  id: number;
  code: string;
  reservationDate: string;
  reservedSeatsIds: number[];
};
