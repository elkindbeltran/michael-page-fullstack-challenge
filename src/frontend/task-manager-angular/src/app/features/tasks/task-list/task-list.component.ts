import { Component, OnInit } from '@angular/core';
import { TaskService } from '../services/task.service';
import { Task } from '../models/task.model';
import { Router } from '@angular/router';
import { User } from '../../users/models/user.model';
import { UserService } from '../../users/services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

  tasks: Task[] = [];

  selectedStatus: string = '';
  selectedUserId: string = '';
  users: User[] = [];
  showProgressBar: boolean = true;

  displayedColumns: string[] = [
    'title',
    'status',
    'userName',
    'additionalData',
    'actions'
  ];

  constructor(
    private taskService: TaskService,
    private userService: UserService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadTasks();
    this.loadUsers();
  }

  loadUsers(): void {
    this.showProgressBar = true;
    this.userService.getUsers().subscribe({
      next: data => {
        this.showProgressBar = false;
        this.users = data
      },
      error: err => {
        this.showProgressBar = false;
        console.error(err);
      }
    });
  }

  loadTasks(): void {
    this.showProgressBar = true;
    this.taskService.getTasks(
      this.selectedUserId || undefined,
      this.selectedStatus || undefined,
      'asc'
    ).subscribe({
      next: data => {
        this.tasks = data;
        this.showProgressBar = false;
      },
      error: err => {
        this.showProgressBar = false;
        console.error('Error loading tasks', err);
      }
    });
  }

  onStatusFilterChange(): void {
    this.loadTasks();
  }

  changeStatus(task: Task, status: number): void {
    this.showProgressBar = true;
    this.taskService.changeStatus(task.id, status)
      .subscribe({
        next: () => this.loadTasks(),
        error: err => {
          this.showProgressBar = false;
          console.error('Error changing status', err);

          let errorMessage = 'Error changing status';

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

  goToTaskCreate(): void {
    this.showProgressBar = true;
    this.router.navigate(['/tasks/new']);
  }
}