import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule, Http, RequestOptions, BrowserXhr } from '@angular/http';
import { FormsModule } from '@angular/forms';

/* 3rd Party Libraries */
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FileUploadModule } from 'ng2-file-upload';
import { NgProgressModule, NgProgressBrowserXhr  } from 'ngx-progressbar';

/* Components */
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { ErrorComponent } from './components/error/error.component';
import { ExpenseReportComponent } from './components/expense-report/expense-report.component';
import { ExpenseItemComponent } from './components/expense-item/expense-item.component';

/* Services */
import { HttpService } from "./services/http.service";
import { ExpenseReportService } from "./services/expense-report.service";

/* Pipes */
import { SortByPipe } from './pipes/sort-by.pipe';
import { FilterPipe } from './pipes/filter.pipe';
import { PaginationComponent } from './components/-pagination/-pagination.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        ErrorComponent,
        ExpenseReportComponent,
        ExpenseItemComponent,
        SortByPipe,
        FilterPipe,
        PaginationComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpModule,
        FormsModule,
        AngularFontAwesomeModule,
        NgbModule.forRoot(),
        FileUploadModule,
        NgProgressModule
    ],
    providers: [
        { provide: Http, useClass: HttpService },
        ExpenseReportService,
        { provide: BrowserXhr, useClass: NgProgressBrowserXhr }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
