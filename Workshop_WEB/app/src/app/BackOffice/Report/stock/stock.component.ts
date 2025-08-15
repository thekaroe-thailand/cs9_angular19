import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { config } from '../../../../config';
import Swal from 'sweetalert2';
import { ReportStockInterface } from '../../Interface/ReportStockInterface';
import { StockInterface } from '../../Interface/StockInterface';
import { ModalComponent } from '../../../Components/modal/modal.component';

@Component({
  selector: 'app-stock',
  standalone: true,
  imports: [ModalComponent],
  templateUrl: './stock.component.html',
  styleUrl: './stock.component.css'
})

export class ReportStockComponent {
  constructor(private http: HttpClient) { }

  reportStocks: ReportStockInterface[] = [];
  stocks: StockInterface[] = [];
  isShowImportStock: boolean = false;
  isShowSale: boolean = false;
  billSaleDetails: any[] = [];

  openModal(productId: number) {
    const url = config.apiUrl + '/api/stock/getByProductId/' + productId;
    this.http.get(url).subscribe((res: any) => {
      this.stocks = res;
      this.isShowImportStock = true;
    });
  }

  closeModal() {
    this.isShowImportStock = false;
  }

  openModalSale(productId: number) {
    const url = config.apiUrl + '/api/billSale/listBillSalesByProductId/' + productId;
    this.http.get(url).subscribe((res: any) => {
      this.billSaleDetails = res;
      this.isShowSale = true;
    });
  }

  closeModalSale() {
    this.isShowSale = false;
  }

  ngOnInit() {
    this.http.get(config.apiUrl + '/api/stock/sumPerProduct').subscribe((res: any) => {
      this.reportStocks = res;
    })
  }
}


