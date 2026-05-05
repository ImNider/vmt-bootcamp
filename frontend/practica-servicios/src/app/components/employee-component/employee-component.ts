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

  agregarEmpleado(){
    const payload: Partial<IEmployee> = {
      name: 'Alejandro Medina',
      email: 'alejandro@mail.com',
      phone: '555-6666',
      position: 'Backend Developer',
      department: 'Engineering',
      salary: '2100'
    };

    this._employeeService.create(payload).subscribe({
      next: (data) => {
        this.employees = [...this.employees, data];
      },
      error: () => {
        this.error = "Error al crear empleado";
      }
    });
  }

  editarEmpleado(id: string){
    
    const payload: Partial<IEmployee> = {
      name: 'Alejandro Medina Editado',
      email: 'alejandro.editado@mail.com',
      phone: '555-0000',
      position: 'Backend Developer Plus',
      department: 'Engineering',
      salary: '3000'
    };

    this._employeeService.update(id, payload).subscribe({
      next: (data) => {
        this.employees = this.employees.map(e => e.id === id ? data : e);
      },
      error: () => {
        this.error = 'Error al editar empleado';
      }
    });
  }

  eliminarEmpleado(id: string){
    this._employeeService.delete(id).subscribe({
      next: () => {
        this.employees = this.employees.filter(e => e.id !== id);
      },
      error: () => {
        this.error = 'Error al eliminar empleado';
      }
    });
  }
}
