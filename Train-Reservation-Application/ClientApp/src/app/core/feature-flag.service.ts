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
  private flags$ = new BehaviorSubject<Set<string>>(new Set());

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  loadFlags(path: string): Observable<Set<string>> {
    return this.httpClient.get<Set<string>>(`${this.apiUrl}${path}`, httpOptions).pipe(
      tap(flagList => {
        this.flags$.next(new Set(flagList));
      })
    );
  }

  getFlags(): Observable<Set<string>> {
    return this.loadFlags('FeatureFlags');
  }

  isEnabled(flag: string): boolean {
    return this.flags$.value.has(flag);
  }
}
