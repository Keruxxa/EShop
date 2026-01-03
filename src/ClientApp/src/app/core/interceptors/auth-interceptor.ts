import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BehaviorSubject, catchError, filter, switchMap, take, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { RefreshTokenResponse } from '../models/refresh-token-response';

const isRefreshing = new BehaviorSubject<boolean>(false);
const refreshToken = new BehaviorSubject<string | null>(null);
const AUTH_URLS = ['/refresh-token', '/sign-in', '/sign-out'];

export const authInterceptor: HttpInterceptorFn = (request, next) => {
  if (AUTH_URLS.some(endpoint => request.url.includes(endpoint))) {
    return next(request);
  }

  const authService = inject(AuthService);
  const token = authService.token;

  if (token) {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  return next(request).pipe(
    catchError(error => {
      if (error.status === 401) {
        if (!isRefreshing.value) {
          isRefreshing.next(true);
          refreshToken.next(null);

          return authService.refreshToken().pipe(
            take(1),
            switchMap((response: RefreshTokenResponse) => {
              refreshToken.next(response.refreshToken);
              isRefreshing.next(false);

              request = request.clone({
                setHeaders: { Authorization: `Bearer ${response.accessToken}` },
              });
              return next(request);
            }),
            catchError(error => {
              isRefreshing.next(false);
              return authService.signOut().pipe(
                take(1),
                switchMap(() => {
                  return next(request);
                }),
              );
            }),
          );
        } else {
          return refreshToken.pipe(
            take(1),
            filter(response => response !== null),
            switchMap((accessToken: string) => {
              request = request.clone({
                setHeaders: {
                  Authorization: `Bearer ${accessToken}`,
                },
              });
              return next(request);
            }),
            catchError(error => {
              isRefreshing.next(false);
              return authService.signOut().pipe(
                take(1),
                switchMap(() => {
                  return next(request);
                }),
              );
            }),
          );
        }
      }
      return throwError(() => error);
    }),
  );
};
