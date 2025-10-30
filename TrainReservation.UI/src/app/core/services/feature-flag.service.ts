import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { tap } from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

export function getFlags(f: FeatureFlagService) {
  return () => f.getFlags().toPromise();
}

@Injectable({
  providedIn: 'root'
})
export class FeatureFlagService {
  private apiUrl: string;
  private flags$ = new BehaviorSubject<Record<string, boolean>>({ });

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  loadFlags(path: string): Observable<Record<string, boolean>> {
    return this.httpClient.get<Record<string, boolean>>(`${this.apiUrl}${path}`, httpOptions).pipe(
      tap(flagList => {
        this.flags$.next(flagList);
      })
    );
  }

  getFlags(): Observable<Record<string, boolean>> {
    return this.loadFlags('FeatureFlags');
  }

  isEnabled(flag: string): boolean {
    return !!this.flags$.value?.[flag];
  }
}
