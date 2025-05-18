import { Component } from '@angular/core';
import { ModalComponent } from '../../Components/modal/modal.component';
import { HttpClient } from '@angular/common/http';
import { config } from '../../../config';
import Swal from 'sweetalert2';
import { PublisherInterface } from '../Interface/PublisherInterface';
import { FormsModule } from '@angular/forms';
import { BookInterface } from '../Interface/BookInterface';

@Component({
  selector: 'app-book',
  standalone: true,
  imports: [ModalComponent, FormsModule],
  templateUrl: './book.component.html',
  styleUrl: './book.component.css'
})
export class BookComponent {
  constructor(private http: HttpClient) { }

  isModalOpen = false;
  publishers: PublisherInterface[] = [];
  books: BookInterface[] = [];

  name: string = '';
  isbn: string = '';
  price: number = 0;
  publisherId: number = 0;
  id: number = 0;

  ngOnInit() {
    this.fetchDataPublisher();
    this.fetchDataBook();
  }

  fetchDataBook() {
    try {
      this.http.get(config.apiUrl + '/api/Book/List').subscribe((res: any) => {
        this.books = res;
      })
    } catch (error: any) {
      Swal.fire({
        title: 'error',
        text: error.message,
        icon: 'error'
      })
    }
  }

  fetchDataPublisher() {
    try {
      this.http.get(config.apiUrl + '/api/Publisher/List').subscribe((res: any) => {
        this.publishers = res;

        if (res.length > 0) {
          this.publisherId = res[0].id;
        }
      });
    } catch (error: any) {
      Swal.fire({
        title: 'error',
        text: error.message,
        icon: 'error'
      })
    }
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  save() {
    try {
      const payload = {
        Name: this.name,
        Isbn: this.isbn,
        Price: this.price,
        PublisherId: this.publisherId
      }

      if (this.id === 0) {
        this.http.post(config.apiUrl + '/api/Book/Create', payload).subscribe((res: any) => {
          this.id = 0;
        })
      } else {
        this.http.put(config.apiUrl + '/api/Book/Update/' + this.id, payload).subscribe((res: any) => { })
      }

      Swal.fire({
        title: 'success',
        text: 'สำเร็จ',
        icon: 'success',
        timer: 1000
      })

      this.closeModal();
      this.fetchDataBook();
    } catch (err: any) {
      Swal.fire({
        title: 'error',
        text: err.message,
        icon: 'error'
      })
    }
  }

  edit(book: BookInterface) {
    this.id = book.id;
    this.name = book.name;
    this.isbn = book.isbn;
    this.price = book.price;
    this.publisherId = book.publisher.id;

    this.openModal();
  }

  async remove(id: number) {
    try {
      const button = await Swal.fire({
        title: 'ยืนยันการลบ',
        text: 'คุณต้องการลบหนังสือนี้หรือไม่?',
        icon: 'question',
        showCancelButton: true,
        showConfirmButton: true
      })

      if (button.isConfirmed) {
        this.http.delete(config.apiUrl + '/api/Book/Remove/' + id).subscribe((res: any) => {
          this.fetchDataBook();
        })
      }
    } catch (error: any) {
      Swal.fire({
        title: 'error',
        text: error.message,
        icon: 'error'
      })
    }
  }

}
