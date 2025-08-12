import { Routes } from '@angular/router';
import { DashboardComponent } from './BackOffice/dashboard/dashboard.component';
import { SigninComponent } from './BackOffice/signin/signin.component';
import { BackofficeLayoutComponent } from './BackOffice/backoffice-layout.component';
import { ChangeProfileComponent } from './BackOffice/change-profile/change-profile.component';
import { BookComponent } from './BackOffice/book/book.component';
import { StockComponent } from './BackOffice/stock/stock.component';
import { ReportStockComponent } from './BackOffice/Report/stock/stock.component';
import { SaleComponent } from './BackOffice/sale/sale.component';
import { BillSaleComponent } from './BackOffice/Report/bill-sale/bill-sale.component';
import { CompanyComponent } from './BackOffice/company/company.component';
import { PersonComponent } from './BackOffice/person/person.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'signin',
        pathMatch: 'full'
    },
    {
        path: 'signin',
        component: SigninComponent
    },
    {
        path: 'backoffice',
        component: BackofficeLayoutComponent,
        children: [
            {
                path: 'dashboard',
                component: DashboardComponent
            },
            {
                path: 'change-profile',
                component: ChangeProfileComponent
            },
            {
                path: 'book',
                component: BookComponent
            },
            {
                path: 'stock',
                component: StockComponent
            },
            {
                path: 'report',
                children: [
                    {
                        path: 'stock',
                        component: ReportStockComponent
                    },
                    {
                        path: 'bill-sale',
                        component: BillSaleComponent
                    }
                ]
            },
            {
                path: 'sale',
                component: SaleComponent
            },
            {
                path: 'company',
                component: CompanyComponent
            },
            {
                path: 'person',
                component: PersonComponent
            }
        ]
    }
];
