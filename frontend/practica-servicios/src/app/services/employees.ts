import { inject, Injectable } from '@angular/core';
import { enviroment } from '../env/enviroment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IEmployee } from '../interfaces/employee';

@Injectable({
  providedIn: 'root',
})
export class EmployeesService {
  private apiUrl = enviroment.apiUrl;
  private _http = inject(HttpClient);

  getAll(): Observable<IEmployee[]> {
    return this._http.get<IEmployee[]>(`${this.apiUrl}/employees`);
  }

  getById(id:string): Observable<IEmployee> {
    return this._http.get<IEmployee>(`${this.apiUrl}/employees/${id}`);
  }
  
}
