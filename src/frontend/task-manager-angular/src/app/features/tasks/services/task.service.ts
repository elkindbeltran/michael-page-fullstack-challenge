import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../models/task.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private apiUrl = `${environment.apiUrl}/tasks`;

  constructor(private http: HttpClient) {}

    getTasks(userId?: string, status?: string, order: string = 'asc'): Observable<Task[]> {

    let params = new HttpParams();

    if (userId) params = params.set('userId', userId);
    if (status) params = params.set('status', status);

    params = params.set('order', order);

    return this.http.get<Task[]>(this.apiUrl, { params });
  }

  createTask(task: Task): Observable<Task> {
    return this.http.post<Task>(this.apiUrl, task);
  }

    changeStatus(taskId: string, status: number): Observable<any> {
    return this.http.put(
      `${this.apiUrl}/${taskId}/status`,
      {
        taskId: taskId,
        status: status
      }
    );
  }
}