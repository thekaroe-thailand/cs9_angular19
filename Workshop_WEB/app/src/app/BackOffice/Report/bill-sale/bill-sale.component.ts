import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { config } from '../../../../config';
import { ModalComponent } from '../../../Components/modal/modal.component';
import dayjs from 'dayjs';

@Component({
  selector: 'app-bill-sale',
  standalone: true,
  imports: [ModalComponent],
  templateUrl: './bill-sale.component.html',
  styleUrl: './bill-sale.component.css'
})
export class BillSaleComponent {
  billSales: any[] = [];
  billSaleDetails: any[] = [];
  billSale: any = {};
  isModalOpen: boolean = false;
  dayjs = dayjs;

  constructor(private http: HttpClient) {
    dayjs.locale('th');
  }

  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
    const url = config.apiUrl + '/api/billSale/list';
    this.http.get(url).subscribe((res: any) => {
      this.billSales = res;
    });
  }

  fetchBillSaleDetails(billId: number) {
    const url = config.apiUrl + `/api/billSale/billSaleDetails/${billId}`;
    this.http.get(url).subscribe((res: any) => {
      this.billSaleDetails = res;
    });
  }

  openModal(bill: any) {
    this.isModalOpen = true;
    this.billSale = bill;

    this.fetchBillSaleDetails(bill.id);
  }

  closeModal() {
    this.isModalOpen = false;
  }
}
