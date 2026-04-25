import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {

  form!: FormGroup;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    this.loading = true;

    this.userService.createUser(this.form.value)
      .subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/users']);
        },
        error: err => {
          this.loading = false;
          console.error('Error creating user', err);

          let errorMessage = 'Error creating user';

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

  goToUsersList(): void {
    this.router.navigate(['/users']);
  }

}
