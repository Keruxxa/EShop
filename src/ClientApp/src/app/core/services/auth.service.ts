import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { environment } from '../../environments/environment.dev';
import { SignInUserModel } from '../../features/auth/models/sign-in-user-model';
import { SignInUserResponseModel } from '../../features/auth/models/sign-in-user-response-model';
import { userId, userToken } from '../constants';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl: string = environment.apiUrl + '/authentication';
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
}
