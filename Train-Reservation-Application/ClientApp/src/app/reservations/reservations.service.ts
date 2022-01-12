import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ModifyReservationViewModel, NewReservationRequest, ResponseService, SeatInCarViewModel, TicketViewModel } from './reservations.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ReservationsService {
  private apiUrl: string;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  post(path: string, body = {}): Observable<ResponseService<TicketViewModel, SeatInCarViewModel[], string>> {
    return this.httpClient.post<ResponseService<TicketViewModel, SeatInCarViewModel[], string>>(`${this.apiUrl}${path}`, JSON.stringify(body), httpOptions);
  }

  put(path: string, body = {}): Observable<ResponseService<TicketViewModel, SeatInCarViewModel[], string>> {
    return this.httpClient.put<ResponseService<TicketViewModel, SeatInCarViewModel[], string>>(`${this.apiUrl}${path}`, JSON.stringify(body), httpOptions);
  }

  makeReservation(newReservation: NewReservationRequest): Observable<ResponseService<TicketViewModel, SeatInCarViewModel[], string>> {
    return this.post(`Reservations`, newReservation);
  }

  modifyReservation(reservationId: number, reservation: ModifyReservationViewModel): Observable<ResponseService<TicketViewModel, SeatInCarViewModel[], string>> {
    return this.put(`Reservations/${reservationId}`, reservation);
  }
}
