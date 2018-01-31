import { Component, OnInit } from '@angular/core';
import { ExpenseReportService } from "../../services/expense-report.service";
import { ActivatedRoute } from "@angular/router";
import { environment } from "../../../environments/environment";
import { FileUploader } from 'ng2-file-upload';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { findWhere, map } from "underscore";
import { NgProgress } from "ngx-progressbar";

@Component({
    selector: 'app-expense-report',
    templateUrl: './expense-report.component.html',
    styleUrls: ['./expense-report.component.css']
})
export class ExpenseReportComponent implements OnInit {
    viewModel: any = {};
    ExpenseReportSubscription;
    public uploader: FileUploader = new FileUploader({ url: environment.UploadURL, autoUpload: true });
    receiptError: string;
    ReceiptsRoot: string = environment.ReceiptsRoot;
    previewPath: string;
    isSelectReceipt: boolean = false;
    selectedItem: number = 0;
    isReverseSort: boolean = false;
    sortBy: string = "Id";

    constructor(
        private expenseReportService: ExpenseReportService,
        private route: ActivatedRoute,
        private modalService: NgbModal,
        public ngProgress: NgProgress) {
        this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
            this.AddReceipt(response);
        };
        this.uploader.onAfterAddingFile = () => {
            this.ngProgress.start();
        }
        this.uploader.onCompleteAll = () => {
            this.ngProgress.done();
        }
    }

    ngOnInit() {
        this.route
            .params
            .map(params => params['ExpenseReportId'])
            .subscribe(id => this.GetExpenseReport(id));
    }

    AddReceipt(data) {
        this.receiptError = null;
        var receipt = JSON.parse(data);
        if (receipt.success) {
            this.viewModel.Receipts.push(receipt.viewModel);
        } else {
            this.receiptError = receipt.error;
        }
    }

    ngOnDestroy() {
        this.ExpenseReportSubscription.unsubscribe();
    }

    private GetExpenseReport(id) {
        this.ExpenseReportSubscription = this.expenseReportService.GetExpenseReport(id).subscribe(t => {
            this.viewModel = t.viewModel;
            this.uploader.options.additionalParameter = { "ExpenseReportId": this.viewModel.ReportId };
        });
    }

    DeleteItem(item) {
        if (confirm("Are you sure you want to delete this item?")) {
            var sub = this.expenseReportService.DeleteExpenseItem(item.ExpenseItemId).subscribe(r => {
                this.viewModel.ExpenseItems.splice(this.viewModel.ExpenseItems.indexOf(item), 1);
                sub.unsubscribe();
            });
        }
    }

    DeleteReceipt(r) {
        if (confirm("Are you sure you want to delete this receipt?")) {
            var sub = this.expenseReportService.DeleteReceipt(r.Id).subscribe(t => {
                this.viewModel.Receipts.splice(this.viewModel.Receipts.indexOf(r), 1);
                //Remove receipts from items that use this receipt
                map(this.viewModel.ExpenseItems, function (obj) {
                    if (obj.Receipt != null && obj.Receipt.Id == r.Id) {
                        obj.Receipt = null;
                    }
                });
                sub.unsubscribe();
            });
        }
    }

    ShowReceiptModal(receiptPreview, Receipt) {
        if (Receipt.IsImage) {
            this.previewPath = Receipt.Path;
            this.modalService.open(receiptPreview, { windowClass: 'large-modal' }).result.then((result) => {

            }, (reason) => {

            });
        } else {
            window.open(this.ReceiptsRoot + "/" + Receipt.Path, "_blank");
        }
    }

    ProcessReceiptClick(receiptPreview, Receipt) {
        if (this.isSelectReceipt) {
            this.SelectReceipt(Receipt);
        } else {
            if (Receipt.IsImage) {
                this.ShowReceiptModal(receiptPreview, Receipt);
            } else {
                window.open(this.ReceiptsRoot + "/" + Receipt.Path, "_blank");
            }
        }
    }

    SelectItem(item) {
        if (this.isSelectReceipt) {
            this.isSelectReceipt = false;
            this.selectedItem = 0;
        } else {
            this.isSelectReceipt = true;
            this.selectedItem = item.ExpenseItemId;
        }
    }

    SelectReceipt(Receipt) {
        var sub = this.expenseReportService.AssignReceiptToItem(this.selectedItem, Receipt.Id).subscribe(t => {
            var ExpenseItem = findWhere(this.viewModel.ExpenseItems, { ExpenseItemId: this.selectedItem });
            ExpenseItem.Receipt = Receipt;
            this.isSelectReceipt = false;
            this.selectedItem = 0;
            sub.unsubscribe();
        });
    }

    RemoveReceipt(Item) {
        var sub = this.expenseReportService.RemoveReceipt(Item.ExpenseItemId).subscribe(t => {
            Item.Receipt = null;
            sub.unsubscribe();
        });
    }

    Sort(Field) {
        this.sortBy = Field;
        this.isReverseSort = !this.isReverseSort; 
    }
}
