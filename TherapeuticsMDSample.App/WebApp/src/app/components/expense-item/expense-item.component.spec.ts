import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgbDatepicker, NgbModule, NgbDateParserFormatter, NgbCalendar, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';

import { FormsModule } from '@angular/forms';
import { ExpenseItemComponent } from './expense-item.component';
import { ActivatedRoute } from "@angular/router";
import { ExpenseReportService } from "../../services/expense-report.service";
import { Location } from '@angular/common';

const fakeActivatedRoute = {
} as ActivatedRoute;

const fakeExpenseReportService = {
} as ExpenseReportService;

const fakeLocation = {
} as Location;

const fakeNgbDateParserFormatter = {
} as NgbDateParserFormatter;


describe('ExpenseItemComponent', () => {
    let component: ExpenseItemComponent;
    let fixture: ComponentFixture<ExpenseItemComponent>;
    let ngbDatepicker: NgbDatepicker

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExpenseItemComponent],
            imports: [FormsModule, NgbModule],
            providers: [
                { provide: ActivatedRoute, useValue: fakeActivatedRoute },
                { provide: ExpenseReportService, useValue: fakeExpenseReportService },
                { provide: Location, useValue: fakeLocation },
                { provide: NgbDateParserFormatter, useValue: fakeNgbDateParserFormatter },
                NgbCalendar, NgbDateAdapter
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExpenseItemComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
});
