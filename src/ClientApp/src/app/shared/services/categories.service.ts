import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.dev';
import { SelectListItem } from '../models/select-list-item.model';

@Injectable()
export class CategoriesService {
  public readonly apiUrl = environment.apiUrl + '/categories';
  constructor(private httpClient: HttpClient) {}

  public getCategories(): Observable<SelectListItem<number>[]> {
    return this.httpClient.get<SelectListItem<number>[]>(`${this.apiUrl}/select-list`);
  }
}
