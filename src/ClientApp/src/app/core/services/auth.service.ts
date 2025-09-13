import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { environment } from '../../environments/environment.dev';
import { SignInUserModel } from '../../features/sign-in/models/sign-in-user-model';
import { SignInUserResponseModel } from '../../features/sign-in/models/sign-in-user-response-model';
import { SignUpUserModel } from '../../features/sign-up/models/sign-up-user-model';
import { SignUpUserResponseModel } from '../../features/sign-up/models/sign-up-user-response-model';
import { ErrorType, ResponseError } from '../../shared/models/response-error.model';
import { userIdKey, userToken } from '../constants';

export interface AuthResponse {
  isSuccess: boolean;
  errorMessage?: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public readonly apiUrl: string = environment.apiUrl + '/authentication';

  constructor(private readonly httpClient: HttpClient) {}

  public signIn(signInUserModel: SignInUserModel): Observable<AuthResponse> {
    return this.httpClient
      .post<SignInUserResponseModel>(`${this.apiUrl}/sign-in`, signInUserModel)
      .pipe(
        tap(signInUserResponseModel => {
          localStorage.setItem(userIdKey, signInUserResponseModel.id);
          localStorage.setItem(userToken, signInUserResponseModel.token);
        }),
        map(() => ({ isSuccess: true })),
        catchError(error => of(this.handleError(error))),
      );
  }

  public signUp(signUpUserModel: SignUpUserModel): Observable<AuthResponse> {
    return this.httpClient
      .post<SignUpUserResponseModel>(`${this.apiUrl}/sign-up`, signUpUserModel)
      .pipe(
        tap(signUpUserResponseModel => {
          localStorage.setItem(userIdKey, signUpUserResponseModel.id);
          localStorage.setItem(userToken, signUpUserResponseModel.token);
        }),
        map(() => ({ isSuccess: true })),
        catchError(error => of(this.handleError(error))),
      );
  }

  get isAuthenticated(): boolean {
    return localStorage.getItem(userToken) !== null;
  }

  private handleError(errorResponse: HttpErrorResponse): AuthResponse {
    const error: ResponseError = errorResponse.error;

    const errorMessage = this.getErrorMessage(error.errorType);

    return {
      isSuccess: false,
      errorMessage: errorMessage,
    };
  }

  private getErrorMessage(errorType: ErrorType): string {
    switch (errorType) {
      case ErrorType.Duplicate:
        return 'Данный Email уже занят';

      default:
        return 'Ошибка произошла на стороне сервера. Попробуйте еще раз';
    }
  }
}
