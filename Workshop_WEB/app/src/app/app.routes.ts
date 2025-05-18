import { Routes } from '@angular/router';
import { DashboardComponent } from './BackOffice/dashboard/dashboard.component';
import { SigninComponent } from './BackOffice/signin/signin.component';
import { BackofficeLayoutComponent } from './BackOffice/backoffice-layout.component';
import { ChangeProfileComponent } from './BackOffice/change-profile/change-profile.component';
import { BookComponent } from './BackOffice/book/book.component';
import { StockComponent } from './BackOffice/stock/stock.component';

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
            }
        ]
    }
];
