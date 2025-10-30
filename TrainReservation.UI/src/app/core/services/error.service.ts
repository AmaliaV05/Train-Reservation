import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ModelError } from '../models/error.model';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
  private errorSubject = new Subject<string>();
  private validationErrors: Record<string, string[]> = {};

  error$ = this.errorSubject.asObservable();

  handle(error: HttpErrorResponse) {
    let message = 'An unexpected error occurred.';
    this.validationErrors = {};

    let serverError = error.error as ModelError;

    if (error.error instanceof ErrorEvent) {
      // Client-side error
      message = `Client-side error: ${error.error.message}`;
    } else if (serverError.errors) {
      // Validation errors
      message = serverError.errorMessage;
      this.validationErrors = serverError.errors;
    } else {
      // General server error
      message = `Server error (${serverError.statusCode}): ${serverError.errorMessage}`;
    }

    this.errorSubject.next(message);
  }  

  getValidationErrors(): Record<string, string[]> {
    const normalized: Record<string, string[]> = {};
    for (const key of Object.keys(this.validationErrors)) {
      normalized[this.normalizeFieldName(key)] = this.validationErrors[key];
    }

    return normalized;
  }

  private normalizeFieldName(field: string): string {
    return field.charAt(0).toLowerCase() + field.slice(1);
  }
}
