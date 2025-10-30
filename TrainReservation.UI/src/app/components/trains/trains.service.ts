import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TrainSearchRequest, TrainViewModel, TrainWithCarsViewModel } from './train.model';
import { CarType } from './enums';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

const header = new HttpHeaders({ 'Content-Type': 'application/json' });

@Injectable({
  providedIn: 'root'
})
export class TrainsService {
  private apiUrl: string;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  getTrain(path: string): Observable<TrainWithCarsViewModel> {
    return this.httpClient.get<TrainWithCarsViewModel>(`${this.apiUrl}${path}`, httpOptions);
  }

  getTrains(path: string, request: TrainSearchRequest): Observable<TrainViewModel[]> {
    const params = new HttpParams().set('date', request.date);
    const httpOptions = this.getHttpOptions(params);

    return this.httpClient.get<TrainViewModel[]>(`${this.apiUrl}${path}`, httpOptions);
  }

  getTrainsWithMultipleSeats(path: string): Observable<number[]> {
    return this.httpClient.get<number[]>(`${this.apiUrl}${path}`, httpOptions);
  }

  getTrainList(request: TrainSearchRequest): Observable<TrainViewModel[]> {
    return this.getTrains(`Trains/filter-trains`, request);
  }

  getTrainWithCars(idTrain: number, date: string, carType: CarType): Observable<TrainWithCarsViewModel> {
    return this.getTrain(`Trains/${idTrain}/${date}/filter-cars/${carType}`);
  }

  getTrainWithMultipleAvailableSeats(idTrain: number, date: string, N: number): Observable<number[]> {
    return this.getTrainsWithMultipleSeats(`Trains/${idTrain}/${date}/filter-cars-by-available-seats/${N}`);
  }

  getHttpOptions(params: HttpParams) {
    return {
      headers: header,
      params: params
    }
  }
}
