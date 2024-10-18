import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, tap, throwError } from 'rxjs';
import { environment } from '../../environments/environment.dev';
import { SignInUserModel } from '../../features/sign-in/models/sign-in-user-model';
import { SignInUserResponseModel } from '../../features/sign-in/models/sign-in-user-response-model';
import { SignUpUserModel } from '../../features/sign-up/models/sign-up-user-model';
import { SignUpUserResponseModel } from '../../features/sign-up/models/sign-up-user-response-model';
import { ErrorType, ReponseError } from '../../shared/models/reponse-error.model';
import { userId, userToken } from '../constants';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public readonly apiUrl: string = environment.apiUrl + '/authentication';

  constructor(private readonly httpClient: HttpClient) {}

  public signIn(signInUserModel: SignInUserModel): Observable<boolean> {
    return this.httpClient
      .post<SignInUserResponseModel>(`${this.apiUrl}/sign-in`, signInUserModel)
      .pipe(
        tap(signInUserResponseModel => {
          localStorage.setItem(userId, signInUserResponseModel.id);
          localStorage.setItem(userToken, signInUserResponseModel.token);
        }),
        map(() => true),
        catchError(() => of(false)),
      );
  }

  public signUp(signUpUserModel: SignUpUserModel): Observable<boolean> {
    return this.httpClient
      .post<SignUpUserResponseModel>(`${this.apiUrl}/sign-up`, signUpUserModel)
      .pipe(
        tap(signUpUserResponseModel => {
          localStorage.setItem(userId, signUpUserResponseModel.id);
          localStorage.setItem(userToken, signUpUserResponseModel.token);
        }),
        map(() => true),
        catchError(this.handleError),
      );
  }

  private handleError = (errorResponse: HttpErrorResponse): Observable<never> => {
    const error: ReponseError = errorResponse.error;

    const errorMessage = this.getErrorMessage(error.errorType);

    return throwError(() => errorMessage);
  };

  private getErrorMessage(errorType: ErrorType): string {
    switch (errorType) {
      case ErrorType.Duplicate:
        return 'Данный Email уже занят';

      default:
        return 'Ошибка произошла на стороне сервера. Попробуйте еще раз';
    }
  }
}
