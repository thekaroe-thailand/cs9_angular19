import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { config } from '../../../config';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  user: string = '';
  level: string = '';

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
    const url = `${config.apiUrl}/api/Person/Info`;
    const headers = {
      'Authorization': `Bearer ${localStorage.getItem(config.tokenKey)}`
    }

    this.http.get(url, { headers }).subscribe((res: any) => {
      this.user = res.name;
      this.level = res.level;
    });
  }

  async signOut() {
    const button = await Swal.fire({
      title: 'ยืนยันการออกจากระบบ',
      text: 'คุณต้องการออกจากระบบหรือไม่',
      icon: 'question',
      showCancelButton: true,
      showConfirmButton: true
    });

    if (button.isConfirmed) {
      localStorage.removeItem(config.tokenKey);
      this.router.navigate(['/']);
    }
  }

}