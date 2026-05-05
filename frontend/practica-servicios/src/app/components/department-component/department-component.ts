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

  agregarDepartamento(){
      const payload: Partial<IDepartment> = {
        name: 'Prueba 1',
        description: 'Descripcion de prueba 1',
        managerName: 'Manager de prueba 1'
      };
  
      this._departmentService.create(payload).subscribe({
        next: (data) => {
          this.departments = [...this.departments, data];
        },
        error: () => {
          this.error = "Error al crear el departamento";
        }
      });
    }
  
    editarDepartamento(id: string){
      
      const payload: Partial<IDepartment> = {
        name: 'Editado: Prueba 1',
        description: 'Editado: Descripcion de prueba 1',
        managerName: 'Editado: Manager de prueba 1'
      };
  
      this._departmentService.update(id, payload).subscribe({
        next: (data) => {
          this.departments = this.departments.map(d => d.id === id ? data : d);
        },
        error: () => {
          this.error = 'Error al editar departamento';
        }
      });
    }
  
    eliminarDepartamento(id: string){
      this._departmentService.delete(id).subscribe({
        next: () => {
          this.departments = this.departments.filter(d => d.id !== id);
        },
        error: () => {
          this.error = 'Error al eliminar departamento';
        }
      });
    }
  
}
