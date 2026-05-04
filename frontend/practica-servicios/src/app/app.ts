import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { EmployeeComponent } from './components/employee-component/employee-component';
import { DepartmentComponent } from './components/department-component/department-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, EmployeeComponent, DepartmentComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('practica-servicios');
}
