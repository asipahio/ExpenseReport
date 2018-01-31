import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormsModule } from '@angular/forms';
import { ExpenseReportComponent } from './expense-report.component';
import { FileUploader } from "ng2-file-upload";

import { FileUploadModule } from "ng2-file-upload"; 
import { SortByPipe } from "../../pipes/sort-by.pipe";
import { FilterPipe } from "../../pipes/filter.pipe";
import { RouterModule } from "@angular/router";
import { APP_BASE_HREF } from '@angular/common';
import { ExpenseReportService } from "../../services/expense-report.service";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { NgbModalStack } from "@ng-bootstrap/ng-bootstrap/modal/modal-stack";
import { NgProgress } from "ngx-progressbar";

const fakeFileUploader = {
   // snapshot: { data: { ... } }
} as FileUploader;

const fakeNgbModal = {
    //snapshot: { data: { ... } }
} as NgbModal;

const fakeNgProgress = {
    //snapshot: { data: { ... } }
} as NgProgress;

describe('ExpenseReportComponent', () => {
    let component: ExpenseReportComponent;
    let fixture: ComponentFixture<ExpenseReportComponent>;
    let uploader: FileUploader = new FileUploader({ url: "", autoUpload: true });
    let expenseReportService: ExpenseReportService;

    class ExpenseReportServiceSpy {
        expenseReport = { "viewModel": { "ReportId": 3, "ReportName": "Report 3", "ExpenseItems": [], "Receipts": [] } };

        GetExpenseReport = jasmine.createSpy('GetExpenseReport').and.callFake(
            (ExpenseReportId) => Promise
                .resolve(true)
                .then(() => Object.assign({}, this.expenseReport))
        );
    }

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExpenseReportComponent, SortByPipe, FilterPipe,],
            imports: [FormsModule, FileUploadModule, RouterModule.forRoot([])],
            providers: [
                { provide: FileUploader, useValue: fakeFileUploader },
                { provide: APP_BASE_HREF, useValue: '/' },
                { provide: ExpenseReportService, useValue: {} },
                { provide: NgbModal, useValue: fakeNgbModal },
                { provide: NgProgress, useValue: fakeNgProgress },
                
            ]
        }).overrideComponent(ExpenseReportComponent, {
            set: {
                providers: [
                    { provide: ExpenseReportService, useClass: ExpenseReportServiceSpy }
                ]
            }
        }).compileComponents();
    }));
});
