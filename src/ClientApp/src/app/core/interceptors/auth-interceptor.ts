import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { userToken, userIdKey } from '../constants';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (request, next) => {
  const router = inject(Router);

  const token = localStorage.getItem(userToken);

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
        localStorage.removeItem(userToken);
        localStorage.removeItem(userIdKey);
        router.navigate(['/login']);
      }
      return throwError(() => error);
    }),
  );
};
