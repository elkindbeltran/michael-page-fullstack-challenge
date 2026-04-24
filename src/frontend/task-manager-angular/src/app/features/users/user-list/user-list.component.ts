import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/user.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[] = [];
  displayedColumns: string[] = ['id', 'name', 'email'];
  showProgressBar: boolean = true;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.showProgressBar = true;
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.showProgressBar = false;
      },
      error: (err) => {
        this.showProgressBar = false;
        console.error(err);
      }
    });
  }
}