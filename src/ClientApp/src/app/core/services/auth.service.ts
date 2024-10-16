import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.dev';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl: string = environment.apiUrl + 'authentication/';

  constructor(private readonly httpClient: HttpClient) {}
}
