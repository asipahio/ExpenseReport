import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { environment } from '../../environments/environment';

@Injectable()
export class ExpenseReportService {

    constructor(private http: Http) { }

    GetExpenseReportsList(startIndex: number, endIndex: number, sortBy: string, sortOrder: string, keyword: string) {
        return this.http.post(environment.APIUrl + "ExpenseReportsData/GetExpenseReportsList", {
            "startIndex": startIndex, "endIndex": endIndex, "sortBy": sortBy, "sortOrder": sortOrder, "keyword": keyword}).map(r => r.json());
    }

    GetExpenseReport(ExpenseReportId) {
        return this.http.post(environment.APIUrl + "ExpenseReportsData/GetExpenseReport", { "ExpenseReportId": ExpenseReportId }).map(r => r.json());
    }

    SaveExpenseReport(Report) {
        return this.http.post(environment.APIUrl + "ExpenseReportsData/SaveExpenseReport", Report).map(t => t.json());
    }

    GetExpenseItem(ExpenseReportId, ExpenseItemId) {
        return this.http.post(environment.APIUrl + "ExpenseItemData/GetExpenseItem", {
            "ExpenseReportId": ExpenseReportId, "ExpenseItemId": ExpenseItemId }).map(r => r.json());
    }

    SaveExpenseItem(ExpenseItem) {
        return this.http.post(environment.APIUrl + "ExpenseItemData/SaveExpenseItem", ExpenseItem).map(t => t.json());
    }

    DeleteExpenseItem(ExpenseItemId) {
        return this.http.post(environment.APIUrl + "ExpenseItemData/DeleteExpenseItem", { "ExpenseItemId": ExpenseItemId }).map(t => t.json());
    }

    DeleteReceipt(ReceiptId) {
        return this.http.post(environment.APIUrl + "ReceiptData/DeleteReceipt", { "ReceiptId": ReceiptId }).map(t => t.json());
    }

    AssignReceiptToItem(ExpenseItemId, ReceiptId) {
        return this.http.post(environment.APIUrl + "ExpenseItemData/AssignReceiptToItem", { "ExpenseItemId": ExpenseItemId, "ReceiptId": ReceiptId }).map(t => t.json());
    }

    RemoveReceipt(ExpenseItemId) {
        return this.http.post(environment.APIUrl + "ExpenseItemData/RemoveReceipt", { "ExpenseItemId": ExpenseItemId }).map(t => t.json());
    }
}
