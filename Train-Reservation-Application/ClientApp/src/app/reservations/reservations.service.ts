import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationsService {
  private apiUrl: string;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  post(path: string, body = {}): Observable<any> {
    const headers = this.getHeaders();
    return this.httpClient.post(
      `${this.apiUrl}${path}`,
      JSON.stringify(body),
      { headers }
    );
  }

  private getHeaders() {
    const headers = {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    } as any;

    return headers;
  }

  private toHttpParams(params): HttpParams {
    if (!params) {
      return new HttpParams();
    }
    return Object.getOwnPropertyNames(params).reduce(
      (p, key) => p.set(key, params[key]),
      new HttpParams()
    );
  }
}
