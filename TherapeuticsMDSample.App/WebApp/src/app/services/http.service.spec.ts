import { TestBed, inject } from '@angular/core/testing';

import { HttpService } from './http.service';
import { XHRBackend, BrowserXhr, ResponseOptions, RequestOptions } from "@angular/http";
import { Router } from "@angular/router";

const fakeXHRBackend = {
    //snapshot: { data: { ... } }
} as XHRBackend;

const fakeRequestOptions = {
    //snapshot: { data: { ... } }
} as RequestOptions;

const fakeRouter = {
    //snapshot: { data: { ... } }
} as Router;

describe('HttpService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
        providers: [HttpService, { provide: XHRBackend, useValue: fakeXHRBackend }, { provide: RequestOptions, useValue: fakeRequestOptions }, { provide: Router, useValue: fakeRouter }]
    });
  });

  it('should be created', inject([HttpService], (service: HttpService) => {
    expect(service).toBeTruthy();
  }));
});
