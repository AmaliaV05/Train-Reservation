import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CarType, TrainViewModel, TrainWithCarsViewModel } from './train.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

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

  getTrains(path: string): Observable<TrainViewModel[]> {
    return this.httpClient.get<TrainViewModel[]>(`${this.apiUrl}${path}`, httpOptions);
  }

  getTrainsWithMultipleSeats(path: string): Observable<number[]> {
    return this.httpClient.get<number[]>(`${this.apiUrl}${path}`, httpOptions);
  }

  getTrainList(selectedDate: Date): Observable<TrainViewModel[]> {
    return this.getTrains(`Trains/filter-trains/${selectedDate}`);
  }

  getTrainWithCars(idTrain: number, date: Date, carType: CarType): Observable<TrainWithCarsViewModel> {
    return this.getTrain(`Trains/${idTrain}/${date}/filter-cars/${carType}`)
  }

  getTrainWithMultipleAvailableSeats(idTrain: number, date: Date, N: number): Observable<number[]> {
    return this.getTrainsWithMultipleSeats(`Trains/${idTrain}/${date}/filter-cars-by-available-seats/${N}`);
  }
}
