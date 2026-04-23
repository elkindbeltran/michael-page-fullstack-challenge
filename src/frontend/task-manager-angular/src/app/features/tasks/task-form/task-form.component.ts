import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService } from '../services/task.service';
import { Router } from '@angular/router';
import { UserService } from '../../users/services/user.service';
import { User } from '../../users/models/user.model';

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
      error: err => console.error(err)
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    this.loading = true;

    this.taskService.createTask(this.form.value)
      .subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/tasks']);
        },
        error: err => {
          this.loading = false;
          console.error('Error creating task', err);
        }
      });
  }
}