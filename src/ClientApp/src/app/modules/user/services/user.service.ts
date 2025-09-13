import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { UserDto } from '../models/user.model';
import { environment } from '../../../environments/environment.dev';
import { UpdateUserRequest } from '../models/update-user-request';

@Injectable()
export class UserService {
  private readonly apiUrl: string = `${environment.apiUrl}/users`;
  private readonly httpClient = inject(HttpClient);

  public getUserById(id: string): Observable<UserDto> {
    return this.httpClient.get<UserDto>(`${this.apiUrl}/${id}`);
  }

  public updateUser(id: string, updateUserRequest: UpdateUserRequest): Observable<void> {
    return this.httpClient.patch<void>(`${this.apiUrl}/${id}`, updateUserRequest).pipe(
      catchError((error, result) => {
        console.log('An error ocured onSave:', error);
        return throwError(() => result);
      }),
    );
  }
}
