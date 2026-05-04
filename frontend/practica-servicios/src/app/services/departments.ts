import { inject, Injectable } from '@angular/core';
import { enviroment } from '../env/enviroment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IDepartment } from '../interfaces/department';

@Injectable({
  providedIn: 'root',
})

export class DepartamentsService {
  
  private apiUrl = enviroment.apiUrl;
  private _http = inject(HttpClient);

  getAll(): Observable<IDepartment[]> {
    return this._http.get<IDepartment[]>(`${this.apiUrl}/departments`);
  }

  getById(id:string): Observable<IDepartment> {
    return this._http.get<IDepartment>(`${this.apiUrl}/departments/${id}`);
  }
  
}
