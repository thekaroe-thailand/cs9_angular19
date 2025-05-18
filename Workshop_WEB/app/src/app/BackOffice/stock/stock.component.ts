import { Component } from '@angular/core';
import { ModalComponent } from '../../Components/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { config } from '../../../config';
import Swal from 'sweetalert2';
import { BookInterface } from '../Interface/BookInterface';
import { StockInterface } from '../Interface/StockInterface';
import dayjs from 'dayjs';

@Component({
  selector: 'app-stock',
  standalone: true,
  imports: [ModalComponent, FormsModule],
  templateUrl: './stock.component.html',
  styleUrl: './stock.component.css'
})
export class StockComponent {
  constructor(private http: HttpClient) { }

  isModalOpen: boolean = false;
  books: BookInterface[] = [];
  stocks: StockInterface[] = [];
  id: number = 0;
  createdDate: string = '';
  bookId: number = 0;
  quantity: number = 0;
  price: number = 0;
  remark: string = '';
  dayjs: any = dayjs;

  ngOnInit() {
    this.fetchDataBooks();
    this.fetchData();
  }

  async fetchDataBooks() {
    try {
      const url = `${config.apiUrl}/api/Book/List`;
      this.http.get<BookInterface[]>(url).subscribe((res: BookInterface[]) => {
        this.books = res;
        this.bookId = this.books[0].id; // set default book id
      });
    } catch (err: any) {
      Swal.fire({
        icon: 'error',
        title: 'error',
        text: err.message,
      });
    }
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  async fetchData() {
    try {
      const url = `${config.apiUrl}/api/Stock/List`;
      this.http.get<StockInterface[]>(url).subscribe((res: StockInterface[]) => {
        this.stocks = res;
      });
    } catch (err: any) {
      Swal.fire({
        icon: 'error',
        title: 'error',
        text: err.message,
      });
    }
  }

  save() {
    try {
      const payload = {
        createdDate: new Date(this.createdDate).toISOString(),
        bookId: this.bookId,
        quantity: this.quantity,
        price: this.price,
        remark: this.remark,
      }

      if (this.id === 0) {
        this.http.post(`${config.apiUrl}/api/Stock/Create`, payload).subscribe((res: any) => { });
      } else {
        this.http.put(`${config.apiUrl}/api/Stock/Update`, payload).subscribe((res: any) => { });
      }

      Swal.fire({
        icon: 'success',
        title: 'บันทึกข้อมูลสำเร็จ',
        text: 'ข้อมูลถูกบันทึกเรียบร้อย',
        timer: 1000,
      });

      this.fetchData();
    } catch (err: any) {
      Swal.fire({
        icon: 'error',
        title: 'error',
        text: err.message,
      });
    }
  }
}
