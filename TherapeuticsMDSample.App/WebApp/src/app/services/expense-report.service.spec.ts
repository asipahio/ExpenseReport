import { TestBed, inject } from '@angular/core/testing';

import { ExpenseReportService } from './expense-report.service';
import { HttpService } from "./http.service";
import { XHRBackend, BrowserXhr, ResponseOptions, RequestOptions,Http } from "@angular/http";
import { Router } from "@angular/router";

const fakeXHRBackend = {
   // snapshot: { data: { ... } }
} as XHRBackend;

const fakeRequestOptions = {
   // snapshot: { data: { ... } }
} as RequestOptions;

const fakeRouter = {
   // snapshot: { data: { ... } }
} as Router;

describe('ExpenseReportService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
        providers: [ExpenseReportService, { provide: Http, useClass: HttpService }, { provide: XHRBackend, useValue: fakeXHRBackend }, { provide: RequestOptions, useValue: fakeRequestOptions }, { provide: Router, useValue: fakeRouter }],
    });
  });

  it('should be created', inject([ExpenseReportService], (service: ExpenseReportService) => {
    expect(service).toBeTruthy();
  }));
});
