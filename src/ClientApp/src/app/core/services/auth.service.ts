import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { environment } from '../../environments/environment.dev';
import { SignInUserModel } from '../../features/sign-in/models/sign-in-user-model';
import { SignInUserResponse } from '../../features/sign-in/models/sign-in-user-response';
import { SignUpUserModel } from '../../features/sign-up/models/sign-up-user-model';
import { SignUpUserResponse } from '../../features/sign-up/models/sign-up-user-response';
import { ErrorType, ResponseError } from '../../shared/models/response-error.model';
import { userIdKey } from '../constants';
import { RefreshTokenResponse } from '../models/refresh-token-response';
import { Router } from '@angular/router';

export interface AuthResponse {
  isSuccess: boolean;
  errorMessage?: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly httpClient = inject(HttpClient);
  private readonly router = inject(Router);

  public readonly apiUrl: string = environment.apiUrl + '/authentication';

  private readonly accessToken = signal<string | null>(null);

  get token(): string | null {
    return this.accessToken();
  }

  public readonly isAuthenticated: boolean = !!this.accessToken;

  public signIn(signInUserModel: SignInUserModel): Observable<AuthResponse> {
    return this.httpClient
      .post<SignInUserResponse>(`${this.apiUrl}/sign-in`, signInUserModel, {
        withCredentials: true,
      })
      .pipe(
        tap((response: SignInUserResponse) => {
          this.accessToken.set(response.accessToken);
          localStorage.setItem(userIdKey, response.id);
        }),
        map(() => ({ isSuccess: true })),
        catchError(error => of(this.handleError(error))),
      );
  }

  public signUp(signUpUserModel: SignUpUserModel): Observable<AuthResponse> {
    return this.httpClient
      .post<SignUpUserResponse>(`${this.apiUrl}/sign-up`, signUpUserModel, {
        withCredentials: true,
      })
      .pipe(
        tap((response: SignUpUserResponse) => {
          this.accessToken.set(response.accessToken);
          localStorage.setItem(userIdKey, response.id);
        }),
        map(() => ({ isSuccess: true })),
        catchError(error => of(this.handleError(error))),
      );
  }

  public refreshToken(): Observable<RefreshTokenResponse> {
    return this.httpClient
      .post<RefreshTokenResponse>(
        `${this.apiUrl}/refresh-token`,
        {},
        {
          withCredentials: true,
        },
      )
      .pipe(
        tap((response: RefreshTokenResponse) => {
          this.accessToken.set(response.accessToken);
        }),
      );
  }

  public signOut(): Observable<void> {
    return this.httpClient
      .post<void>(`${this.apiUrl}/sign-out`, {}, { withCredentials: true })
      .pipe(
        tap(() => {
          localStorage.clear();
          this.router.navigate(['/sign-in']);
        }),
      );
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
      case ErrorType.BadRequest:
        return 'Неверный Email или пароль';

      default:
        return 'Ошибка произошла на стороне сервера. Попробуйте повторить запрос позже';
    }
  }
}
