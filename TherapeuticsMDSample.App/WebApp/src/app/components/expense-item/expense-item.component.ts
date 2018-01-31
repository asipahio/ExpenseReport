import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { ExpenseReportService } from "../../services/expense-report.service";
import { Location } from '@angular/common';
import { NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-expense-item',
    templateUrl: './expense-item.component.html',
    styleUrls: ['./expense-item.component.css']
})
export class ExpenseItemComponent implements OnInit {
    ExpenseReportId: number;
    ExpenseItemId: number;
    ExpenseItemSubscription;
    viewModel: any = {};
    Type: string = "Add";

    constructor(
        private route: ActivatedRoute,
        private expenseReportService: ExpenseReportService,
        private location: Location,
        private ngbDateParserFormatter: NgbDateParserFormatter) { }

    ngOnInit() {
        this.route
            .params
            .map(params => {
                this.ExpenseReportId = params['ExpenseReportId'];
                this.ExpenseItemId = params["ExpenseItemId"]
            })
            .subscribe(id => this.GetExpenseItem());
    }

    ngOnDestroy() {
        this.ExpenseItemSubscription.unsubscribe();
    }

    GetExpenseItem() {
        if (this.ExpenseItemId) { this.Type = "Edit"; }
        this.ExpenseItemSubscription = this.expenseReportService.GetExpenseItem(this.ExpenseReportId, this.ExpenseItemId).subscribe(t => {
            this.viewModel = t.viewModel;
            if (this.viewModel.ExpenseItemId == 0) {
                var today = new Date();
                this.viewModel.ExpenseDate = today.toString();
            }
            this.viewModel.ExpenseDate = this.ngbDateParserFormatter.parse(this.viewModel.ExpenseDate);
        });
    }

    onSubmit(viewModel) {
        let formattedDate = this.ngbDateParserFormatter.format(viewModel.ExpenseDate);
        viewModel.ExpenseDate = formattedDate;
        viewModel.ExpenseCategory = null;
        var subs = this.expenseReportService.SaveExpenseItem(viewModel).subscribe(res => {
            if (res.success) {
                this.location.back();
            } else {

            }
            subs.unsubscribe();
        });
    }
}
