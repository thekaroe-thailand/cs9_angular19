import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { config } from '../../../config';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';
import { ModalComponent } from '../../Components/modal/modal.component';

@Component({
  selector: 'app-sale',
  standalone: true,
  imports: [FormsModule, ModalComponent],
  templateUrl: './sale.component.html',
  styleUrl: './sale.component.css'
})
export class SaleComponent {
  constructor(private http: HttpClient) { }
  isbn: string = '';
  saleTemps: any[] = [];
  amount: number = 0;
  isModalOpen: boolean = false;
  receiveAmount: number = 0;

  ngOnInit() {
    this.fetchData();
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  fetchData() {
    try {
      this.amount = 0;
      const url = config.apiUrl + '/api/saleTemp/list';
      this.http.get(url).subscribe((res: any) => {
        this.saleTemps = res;

        for (const item of this.saleTemps) {
          this.amount += item.total;
        }
      })
    } catch (err: any) {
      Swal.fire({
        title: 'ผิดพลาด',
        text: err.message,
        icon: 'error'
      })
    }
  }

  save() {
    const url = config.apiUrl + '/api/saleTemp/Create';
    const payload = {
      isbn: this.isbn
    }

    this.http.post(url, payload).subscribe((res: any) => {
      this.fetchData();
    })
  }

  async delete(id: number) {
    try {
      const button = await Swal.fire({
        icon: 'warning',
        title: 'ยืนยันการลบ',
        text: 'คุณต้องการลบข้อมูลนี้หรือไม่',
        showCancelButton: true,
        showConfirmButton: true
      })

      if (button.isConfirmed) {
        const url = config.apiUrl + `/api/saleTemp/Delete/${id}`;
        this.http.delete(url).subscribe((res: any) => {
          this.fetchData();
        })
      }
    } catch (err: any) {
      Swal.fire({
        title: 'ผิดพลาด',
        text: err.message,
        icon: 'error'
      })
    }
  }

  async updateQty(id: number, qty: number) {
    const url = config.apiUrl + `/api/saleTemp/UpdateQty/${id}`;
    const payload = {
      qty: qty
    }
    this.http.put(url, payload).subscribe((res: any) => {
      this.fetchData();
    })
  }

  handleUpQty(id: number) {
    const item = this.saleTemps.find(item => item.id === id);
    if (item) {
      this.updateQty(id, item.qty + 1);
    }
  }

  handleDownQty(id: number) {
    const item = this.saleTemps.find(item => item.id === id);
    if (item) {
      this.updateQty(id, item.qty - 1);
    }
  }

  payFull() {
    this.receiveAmount = this.amount;
  }

  async endSale() {
    try {
      const url = config.apiUrl + '/api/saleTemp/endSale';
      const payload = {
        Amount: this.amount,
        ReceiveAmount: this.receiveAmount
      }
      this.http.post(url, payload).subscribe((res: any) => {
        this.fetchData();
        this.closeModal();
      })
    } catch (err: any) {
      Swal.fire({
        title: 'ผิดพลาด',
        text: err.message,
        icon: 'error'
      })
    }
  }
}
