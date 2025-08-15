import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { DashboardInterface } from '../Interface/DashboardInterface';
import { config } from '../../../config';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  dashboardData: DashboardInterface | null = null;
  loading = true;
  error = false;
  month: number = 0;
  year: number = 0;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    const currentDate = new Date();
    this.month = currentDate.getMonth() + 1;
    this.year = currentDate.getFullYear();
    this.loadDashboardData();
  }

  loadDashboardData() {
    const url = `${config.apiUrl}/api/BillSale/dashboard/${this.month}/${this.year}`;
    this.http.get<DashboardInterface>(url)
      .subscribe({
        next: (data) => {
          this.dashboardData = data;
          this.loading = false;
        },
        error: (error) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: (error as Error).message
          });

          this.error = true;
          this.loading = false;
        }
      });
  }

  getChartData() {
    if (!this.dashboardData) return [];

    return this.dashboardData.arrSumSalePerDay.map((value, index) => ({
      day: index + 1,
      sales: value
    }));
  }

  getMaxSales() {
    if (!this.dashboardData) return 0;
    return Math.max(...this.dashboardData.arrSumSalePerDay);
  }
}