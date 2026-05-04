import { Component, inject } from '@angular/core';
import { EmployeesService } from '../../services/employees';
import { IEmployee } from '../../interfaces/employee';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employee-component',
  imports: [FormsModule],
  templateUrl: './employee-component.html',
  styleUrl: './employee-component.scss',
})
export class EmployeeComponent {
  private _employeeService = inject(EmployeesService);
  employees: IEmployee[] = [];
  loading = false;
  error = '';
  searchId: string = '';
  
  ngOnInit(){
    this.cargarEmpleados();
  }

  cargarEmpleados() {
    this.loading = true;
    this.error = '';

    this._employeeService.getAll().subscribe({
      next: (data) => {
        this.employees = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar los empleados';
        this.loading = false;
      }
    });
  }

}
