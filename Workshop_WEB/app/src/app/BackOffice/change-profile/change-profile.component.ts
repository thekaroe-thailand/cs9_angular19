import { Component, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { config } from '../../../config';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';
import { BackofficeLayoutComponent } from '../backoffice-layout.component';

@Component({
  selector: 'app-change-profile',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './change-profile.component.html',
  styleUrl: './change-profile.component.css'
})
export class ChangeProfileComponent {
  private layout = inject(BackofficeLayoutComponent);

  constructor(private http: HttpClient) { }

  name: string = '';
  username: string = '';
  password: string = '';
  confirmPassword: string = '';

  ngOnInit() {
    const apiUrl = config.apiUrl + '/api/Person/PersonInfo';
    const token = localStorage.getItem(config.tokenKey);
    const headers = {
      'Authorization': `Bearer ${token}`
    }

    this.http.get(apiUrl, { headers }).subscribe((res: any) => {
      this.name = res.name;
      this.username = res.username;
    });
  }

  save() {
    if (this.password !== this.confirmPassword) {
      Swal.fire({
        icon: 'info',
        title: 'รหัสผ่านไม่ตรงกัน',
        text: 'กรุณากรอกรหัสผ่านใหม่ให้ถูกต้อง',
      });
      return;
    }

    const payload = {
      name: this.name,
      username: this.username,
      password: this.password,
    }

    const token = localStorage.getItem(config.tokenKey);
    const headers = {
      'Authorization': `Bearer ${token}`
    }

    this.http.put(config.apiUrl + '/api/Person/ChangeProfile', payload, { headers }).subscribe((res: any) => {
      Swal.fire({
        icon: 'success',
        title: 'บันทึกข้อมูลสำเร็จ',
        text: 'ข้อมูลของคุณได้ถูกบันทึกเรียบร้อย',
        timer: 2000
      });

      // เรียกใช้ refreshSidebar จาก layout component โดยตรง
      this.layout.refreshSidebar();
    });
  }
}
