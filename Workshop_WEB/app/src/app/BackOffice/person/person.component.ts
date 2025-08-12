import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PersonInterface } from '../Interface/PersonInterface';
import { config } from '../../../config';
import Swal from 'sweetalert2';
import { ModalComponent } from '../../Components/modal/modal.component';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [FormsModule, ModalComponent],
  templateUrl: './person.component.html',
  styleUrl: './person.component.css'
})
export class PersonComponent {
  name: string = '';
  username: string = '';
  password: string = '';
  id: number = 0;
  persons: PersonInterface[] = [];
  isModalOpen: boolean = false;
  age: number = 0;
  confirmPassword: string = '';

  constructor(private http: HttpClient) { }

  fetchData() {
    try {
      const url = `${config.apiUrl}/api/Person/List`;
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem(config.tokenKey)}`
      }

      this.http.get<PersonInterface[]>(url, { headers }).subscribe((data) => {
        this.persons = data;
      });
    } catch (error) {
      Swal.fire({
        icon: 'error',
        title: 'เกิดข้อผิดพลาด',
        text: (error as Error).message
      });
    }
  }

  ngOnInit() {
    this.fetchData();
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  save() {
    try {
      if (this.password !== this.confirmPassword) {
        Swal.fire({
          icon: 'error',
          title: 'เกิดข้อผิดพลาด',
          text: 'รหัสผ่านไม่ตรงกัน'
        });

        return;
      }

      let url = `${config.apiUrl}/api/Person/Create`;
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem(config.tokenKey)}`
      }
      const payload = {
        Name: this.name,
        Username: this.username,
        Password: this.password,
        Age: this.age
      }

      if (this.id > 0) {
        url = `${config.apiUrl}/api/Person/Update/${this.id}`;
      } else {
        url = `${config.apiUrl}/api/Person/Create`;
      }

      this.http.post(url, payload, { headers }).subscribe((data) => {
        this.fetchData();

        Swal.fire({
          icon: 'success',
          title: 'สำเร็จ',
          text: 'บันทึกข้อมูลสำเร็จ',
          timer: 1500
        });

        this.closeModal();
        this.id = 0;
        this.name = '';
        this.username = '';
        this.age = 0;
        this.password = '';
        this.confirmPassword = '';
      });
    } catch (error) {
      Swal.fire({
        icon: 'error',
        title: 'เกิดข้อผิดพลาด',
        text: (error as Error).message
      });
    }
  }

  async delete(id: number) {
    try {
      const buttonConfirm = await Swal.fire({
        title: 'ยืนยันการลบ',
        text: 'คุณต้องการลบข้อมูลนี้หรือไม่',
        icon: 'question',
        showCancelButton: true,
        showConfirmButton: true
      });

      if (buttonConfirm.isConfirmed) {
        const url = `${config.apiUrl}/api/Person/Delete/${id}`;
        const headers = {
          'Authorization': `Bearer ${localStorage.getItem(config.tokenKey)}`
        }

        this.http.delete(url, { headers }).subscribe((data) => {
          this.fetchData();
        });
      }
    } catch (error) {
      Swal.fire({
        icon: 'error',
        title: 'เกิดข้อผิดพลาด',
        text: (error as Error).message
      });
    }
  }

  edit(id: number) {
    this.id = id;

    const person = this.persons.find(p => p.id === id);

    if (person) {
      this.name = person.name;
      this.username = person.username;
      this.age = person.age;
    }

    this.openModal();
  }
}
