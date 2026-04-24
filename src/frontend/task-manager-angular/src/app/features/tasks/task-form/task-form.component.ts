import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService } from '../services/task.service';
import { Router } from '@angular/router';
import { UserService } from '../../users/services/user.service';
import { User } from '../../users/models/user.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html'
})
export class TaskFormComponent implements OnInit {

  form!: FormGroup;
  loading = false;
  users: User[] = [];

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private userService: UserService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      title: ['', Validators.required],
      additionalData: [''],
      userId: ['', Validators.required]
    });

    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: data => this.users = data,
      error: err => {
        console.error('Error loading users', err);

        let errorMessage = 'Error loading users';

        if (err?.error?.Errors?.length) {
          errorMessage = err.error.Errors
            .map((e: any) => `${e.PropertyName}: ${e.ErrorMessage}`)
            .join('\n');
        } else if (err?.error?.Message) {
          errorMessage = err.error.Message;
        }

        this.snackBar.open(errorMessage, 'Close', {
          duration: 6000,
          panelClass: ['snackbar-error']
        });
      }
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    this.loading = true;

    if (this.form.get('additionalData')?.value === '')
      this.form.get('additionalData')?.setValue('{}');

    this.taskService.createTask(this.form.value)
      .subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/tasks']);
        },
        error: err => {
          this.loading = false;
          console.error('Error creating task', err);

          let errorMessage = 'Error creating task';

          if (err?.error?.Errors?.length) {
            errorMessage = err.error.Errors
              .map((e: any) => `${e.PropertyName}: ${e.ErrorMessage}`)
              .join('\n');
          } else if (err?.error?.Message) {
            errorMessage = err.error.Message;
          }

          this.snackBar.open(errorMessage, 'Close', {
            duration: 6000,
            panelClass: ['snackbar-error']
          });
        }
      });
  }

  goToTaskList(): void {
    this.router.navigate(['/tasks']);
  }  
}