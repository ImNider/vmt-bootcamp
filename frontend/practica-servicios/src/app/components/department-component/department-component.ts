import { Component, inject } from '@angular/core';
import { DepartamentsService } from '../../services/departments';
import { IDepartment } from '../../interfaces/department';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-department-component',
  imports: [FormsModule],
  templateUrl: './department-component.html',
  styleUrl: './department-component.scss',
})
export class DepartmentComponent {
  private _departmentService = inject(DepartamentsService);
  departments: IDepartment[] = [];
  loading = false;
  error = '';
  searchId: string = '';
  
  ngOnInit(){
    this.cargarDepartamentos();
  }

  cargarDepartamentos() {
    this.loading = true;
    this.error = '';

    this._departmentService.getAll().subscribe({
      next: (data) => {
        this.departments = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar los departamentos';
        this.loading = false;
      }
    });
  }
}
