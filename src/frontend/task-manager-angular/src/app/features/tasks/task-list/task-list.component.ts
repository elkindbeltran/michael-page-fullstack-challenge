import { Component, OnInit } from '@angular/core';
import { TaskService } from '../services/task.service';
import { Task } from '../models/task.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html'
})
export class TaskListComponent implements OnInit {

  tasks: Task[] = [];

  // filtros
  selectedStatus: string = '';
  selectedUserId: string = '';

  displayedColumns: string[] = [
    'title',
    'status',
    'userId',
    'additionalData',
    'actions'
  ];

  constructor(
    private taskService: TaskService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks(
      this.selectedUserId || undefined,
      this.selectedStatus || undefined,
      'asc'
    ).subscribe({
      next: data => this.tasks = data,
      error: err => console.error('Error loading tasks', err)
    });
  }

  onStatusFilterChange(): void {
    this.loadTasks();
  }

  changeStatus(task: Task, status: number): void {
    this.taskService.changeStatus(task.id, status)
      .subscribe({
        next: () => this.loadTasks(),
        error: err => console.error('Error changing status', err)
      });
  }

  goToCreate(): void {
    this.router.navigate(['/tasks/new']);
  }  
}