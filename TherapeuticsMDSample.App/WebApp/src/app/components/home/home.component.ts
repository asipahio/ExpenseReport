import { Component, OnInit } from '@angular/core';
import { ExpenseReportService } from "../../services/expense-report.service";
import { reduce } from "underscore";
import { environment } from "../../../environments/environment";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    viewModel: any = {};
    expenseReportListSubscription;
    pages: number[] = new Array<number>();
    currentPage: number = 1;
    startIndex: number;
    endIndex: number;
    sortBy: string = "ReportId";
    sortOrder: string = "Asc";
    keyword: string;
    totalPages: number = 1;

    constructor(private expenseReportService: ExpenseReportService) { }

    ngOnInit() {
        this.startIndex = 0;
        this.endIndex = environment.resultsPerPage;

        this.expenseReportListSubscription = this.expenseReportService.GetExpenseReportsList(this.startIndex, this.endIndex, this.sortBy, this.sortOrder, this.keyword).subscribe(t => {
            this.viewModel = t.viewModel;
            this.totalPages = this.viewModel.TotalReports / environment.resultsPerPage;
        });
    }

    ngOnDestroy() {
        this.expenseReportListSubscription.unsubscribe();
    }

    AddExpenseReport() {
        if (this.viewModel.ExpenseReports == null) { this.viewModel.ExpenseReports = []; }
        this.viewModel.ExpenseReports.push({ "ReportId": 0, "ReportName": "", "isEdit": true, "isNew": true });
    }

    CancelEdit(Report) {
        if (Report.isNew) {
            this.viewModel.ExpenseReports.splice(this.viewModel.ExpenseReports.indexOf(Report), 1);
        } else {
            Report.isEdit = false;
            Report.ReportName = Report.OrgReportName;
        }
    }

    ShowEdit(Report) {
        Report.OrgReportName = Report.ReportName;
        Report.isEdit = true;
    }

    SaveReport(Report) {
        Report.isNew = false;
        var sub = this.expenseReportService.SaveExpenseReport(Report).subscribe(res => {
            if (res.success) {
                Report.isEdit = false;
            } else {

            }
            sub.unsubscribe();
        });
    }

    ChangePage(Page) {
        if (this.totalPages < Page || Page < 1) {
            return;
        }
        this.currentPage = Page;
        this.startIndex = (Page - 1) * environment.resultsPerPage;
        this.endIndex = this.startIndex + environment.resultsPerPage;
        this.expenseReportListSubscription.unsubscribe();
        this.expenseReportListSubscription = this.expenseReportService.GetExpenseReportsList(this.startIndex, this.endIndex, this.sortBy, this.sortOrder, this.keyword).subscribe(t => {
            this.viewModel = t.viewModel;
            this.currentPage = Page;
        });
    }

    ChangeSort(Field) {
        this.startIndex = 0;
        this.endIndex = environment.resultsPerPage;
        if (this.sortBy == Field) {
            this.sortOrder = this.sortOrder == "Desc" ? "Asc" : "Desc";
        }
        this.sortBy = Field;
        this.ChangePage(1);
    }

    filterResults(keyword) {
        this.keyword = keyword;
        this.ChangePage(1);
    }

    ExpenseTotal(r) {
        return reduce(r.ExpenseItems, function (m, x) { return m + x.ExpenseAmount; }, 0);
    }

}
