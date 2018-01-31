import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { AppRoutingModule } from "./app-routing.module";

/* Components */
import { HomeComponent } from './components/home/home.component';
import { ErrorComponent } from './components/error/error.component';
import { ExpenseReportComponent } from './components/expense-report/expense-report.component';
import { ExpenseItemComponent } from './components/expense-item/expense-item.component';

import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FileUploadModule } from 'ng2-file-upload';
import { NgProgressModule, NgProgressBrowserXhr } from 'ngx-progressbar';

import { HttpModule, Http, RequestOptions, BrowserXhr } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { APP_BASE_HREF } from '@angular/common';
import { SortByPipe } from './pipes/sort-by.pipe';
import { FilterPipe } from './pipes/filter.pipe';
import { HttpService } from "./services/http.service";
import { PaginationComponent } from "./components/-pagination/-pagination.component";

describe('AppComponent', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                AppComponent,
                HomeComponent,
                ErrorComponent,
                ExpenseReportComponent,
                ExpenseItemComponent,
                SortByPipe,
                FilterPipe,
                PaginationComponent
            ],
            imports: [
                AppRoutingModule,
                HttpModule,
                FormsModule,
                AngularFontAwesomeModule,
                NgbModule.forRoot(),
                FileUploadModule,
                NgProgressModule
            ],
            providers: [
                { provide: APP_BASE_HREF, useValue: '/' },
                { provide: Http, useClass: HttpService },
                { provide: BrowserXhr, useClass: NgProgressBrowserXhr }
            ]
        }).compileComponents();
    }));
    it('should create the app', async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));
    it(`should have as title 'app'`, async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app.title).toEqual('app');
    }));
    //it('should render title in a h1 tag', async(() => {
    //    const fixture = TestBed.createComponent(AppComponent);
    //    fixture.detectChanges();
    //    const compiled = fixture.debugElement.nativeElement;
    //    expect(compiled.querySelector('h1').textContent).toContain('Welcome to app!');
    //}));
});
