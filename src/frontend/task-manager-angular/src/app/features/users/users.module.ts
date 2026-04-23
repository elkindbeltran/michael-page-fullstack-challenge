import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Components
import { UserListComponent } from './user-list/user-list.component';

// Routing
import { UsersRoutingModule } from './users-routing.module';

// Angular Material
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  declarations: [
    UserListComponent
  ],
  imports: [
    CommonModule,

    // Forms (por si luego agregas filtros o form)
    FormsModule,
    ReactiveFormsModule,

    // Routing feature
    UsersRoutingModule,

    // Material
    MatTableModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule
  ]
})
export class UsersModule { }