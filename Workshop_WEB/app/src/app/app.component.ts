import { Component } from '@angular/core';
import { Router, RouterModule, NavigationEnd } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';
import { config } from '../config';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  constructor(private http: HttpClient, private router: Router) { }

  username: string = '';
  password: string = '';
  token: string = '';

  ngOnInit() {
    this.token = localStorage.getItem(config.tokenKey) || '';

    if (this.token) {
      this.router.events.subscribe((event) => {
        if (event instanceof NavigationEnd) {
          this.router.navigate([this.router.url]);
        }
      });
    } else {
      this.router.navigate(['/signin']);
    }
  }

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
