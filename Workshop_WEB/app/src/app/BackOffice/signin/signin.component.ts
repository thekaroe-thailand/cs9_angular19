import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { config } from '../../../config';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.css'
})
export class SigninComponent {
  constructor(private http: HttpClient, private router: Router) { }

  username: string = '';
  password: string = '';

  signIn() {
    try {
      const payload = {
        username: this.username,
        password: this.password
      }
      this.http.post(`${config.apiUrl}/api/Person/SignIn`, payload).subscribe({
        next: (res: any) => {
          if (res.token !== null) {
            localStorage.setItem(config.tokenKey, res.token);
            this.router.navigate(['/backoffice/dashboard']);
          }
        },
        error: ((error: any) => {
          if (error.status === 401) {
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: 'Invalid username or password'
            });
          }
        })
      });
    } catch (error: any) {
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: error.message
      })
    }
  }
}
