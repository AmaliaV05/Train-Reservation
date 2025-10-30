import { Injectable } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors } from '@angular/forms';

export interface ValidationErrorList {
  message: string,
  key: string,
  errorKey: string
}

@Injectable({
  providedIn: 'root'
})
export class ValidationErrorsHandler {
  errorList!: ValidationErrorList[];

  public parseFormErrors(control: AbstractControl, parentKey: string = ''): ValidationErrorList[] {
    this.errorList = [];

    if (control instanceof FormGroup) {
      Object.keys(control.controls).forEach((key: string) => {
        const childControl = control.get(key)!;
        const fullKey = parentKey ? `${parentKey}.${key}` : key; // track full path

        if (childControl instanceof FormGroup) {
          this.errorList.push(...this.parseFormErrors(childControl, fullKey));
        }
        else if (ValidationErrorsHandler.hasError(childControl)) {
          this.errorList.push(...this.setErrorMessage(childControl.errors as ValidationErrors, fullKey));
        }
      });
    }
    else if (control instanceof FormControl) {
      if (ValidationErrorsHandler.hasError(control)) {
        this.errorList.push(...this.setErrorMessage(control.errors as ValidationErrors, parentKey));
      }
    }

    return this.errorList;
  }

  public getValidationErrorMessage(errors: ValidationErrorList[], key: string): ValidationErrorList[] {
    return errors.filter(error => error.key.includes(key));
  }

  public getErrorMessage(list: ValidationErrorList[], key: string, errorKey: string): string | undefined {
    return list.find(err => err.key === key && err.errorKey === errorKey)?.message;
  }

  private static hasError(control: AbstractControl): boolean {
    return control.invalid && (control.dirty || control.touched);
  }

  private setErrorMessage(errors: ValidationErrors | null, key: string): ValidationErrorList[] {
    Object.keys(errors).forEach(errorKey => {
      // If message defined in map, use it
      const message = this.errorMessages[errorKey]
        || (errors[errorKey]?.message ?? 'Invalid value'); // fallback for custom validators

      this.errorList.push({ message, key, errorKey });
    });

    return this.errorList;
  }

  // TODO: Add more error messages as needed
  private readonly errorMessages: { [key: string]: string } = {
    required: 'Required field',
    email: 'Invalid email',
    pattern: 'Invalid format',
    matDatepickerParse: 'Invalid date format',
    matDatepickerMin: 'Invalid start date',
    matDatepickerMax: 'Invalid end date',
    minlength: 'Value is too short',
    maxlength: 'Value is too long',
  };
}
