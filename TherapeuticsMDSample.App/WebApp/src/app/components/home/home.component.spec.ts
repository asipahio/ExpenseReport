import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { ExpenseReportService } from "../../services/expense-report.service";
import { FormsModule } from "@angular/forms";
import { ActivatedRoute, RouterModule } from "@angular/router";
import { APP_BASE_HREF } from '@angular/common';

const fakeActivatedRoute = {
    //snapshot: { data: { ... } }
} as ActivatedRoute;

describe('HomeComponent', () => {
    let component: HomeComponent;
    let fixture: ComponentFixture<HomeComponent>;
    let expenseReportService: ExpenseReportService;
    let componentExpenseReportService: ExpenseReportService;
    let comp: HomeComponent;
    let ersSpy: ExpenseReportServiceSpy;
    class ExpenseReportServiceSpy {
        expenseReportList = { "viewModel": { "ExpenseReports": [{ "ReportId": 3, "ReportName": "Report 3", "ExpenseItems": [], "Receipts": [] }, { "ReportId": 2, "ReportName": "Report 2", "ExpenseItems": [], "Receipts": [] }], "TotalReports": 3 }, "success": true };

        GetExpenseReportsList = jasmine.createSpy('GetExpenseReportsList').and.callFake(
            () => Promise
                .resolve(true)
                .then(() => Object.assign({}, this.expenseReportList))
        );
    }

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [HomeComponent],
            imports: [FormsModule, RouterModule.forRoot([])],
            providers: [
                { provide: ExpenseReportService, useValue: {} },
                { provide: APP_BASE_HREF, useValue: '/' },
                { provide: ActivatedRoute, useValue: fakeActivatedRoute },

            ]
        }).compileComponents();

        

    }));

});
