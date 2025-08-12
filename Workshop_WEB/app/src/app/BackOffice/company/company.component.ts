import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { config } from '../../../config';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-company',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './company.component.html',
  styleUrl: './company.component.css'
})
export class CompanyComponent {
  name: string = '';
  address: string = '';
  phone: string = '';
  email: string = '';
  taxId: string = '';
  id: number = 0;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    const url = `${config.apiUrl}/api/Company/Info`;
    const headers = {
      'Authorization': `Bearer ${localStorage.getItem(config.tokenKey)}`
    }

    this.http.get<any>(url, { headers }).subscribe((data) => {
      this.name = data.name;
      this.address = data.address;
      this.phone = data.phone;
      this.email = data.email;
      this.taxId = data.taxId;
      this.id = data.id;
    });
  }

  save() {
    let url = `${config.apiUrl}/api/Company/Create`;
    const headers = {
      'Authorization': `Bearer ${localStorage.getItem(config.tokenKey)}`
    }
    const payload = {
      Name: this.name,
      Address: this.address,
      Phone: this.phone,
      Email: this.email,
      TaxId: this.taxId,
      Id: this.id
    }

    if (this.id > 0) {
      url = `${config.apiUrl}/api/Company/Update/${this.id}`;
      this.http.put<any>(url, payload, { headers }).subscribe((data) => {
        Swal.fire({
          title: 'Success',
          text: 'บันทึกข้อมูลสำเร็จ',
          icon: 'success',
          timer: 1000
        });
      });
    } else {
      this.http.post<any>(url, payload, { headers }).subscribe((data) => {
        Swal.fire({
          title: 'Success',
          text: 'บันทึกข้อมูลสำเร็จ',
          icon: 'success',
          timer: 1000
        });
      });
    }
  }
}
