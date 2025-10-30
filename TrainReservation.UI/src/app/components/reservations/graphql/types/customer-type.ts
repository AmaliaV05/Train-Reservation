import { Reservation } from "./reservation-type";


export type Customer = {
    id: number;
    socialSecurityNumber: string;
    name: string;
    email: string;
    reservations: Reservation[];
};
