import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from "./components/home/home.component";
import { ErrorComponent } from "./components/error/error.component";
import { ExpenseReportComponent } from "./components/expense-report/expense-report.component";
import { ExpenseItemComponent } from "./components/expense-item/expense-item.component";

const routes: Routes = [
    {
        path: 'Home',
        component: HomeComponent
    },
    {
        path: 'ExpenseReport/:ExpenseReportId',
        component: ExpenseReportComponent
    },
    {
        path: 'ExpenseItem/:ExpenseReportId',
        component: ExpenseItemComponent
    },
    {
        path: 'ExpenseItem/:ExpenseReportId/:ExpenseItemId',
        component: ExpenseItemComponent
    },
    {
        path: 'Error',
        component: ErrorComponent
    },
    {
        path: '',
        redirectTo: '/Home',
        pathMatch: 'full',
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
