import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges, SimpleChange } from '@angular/core';

@Component({
    selector: 'app--pagination',
    templateUrl: './-pagination.component.html',
    styleUrls: ['./-pagination.component.css']
})
export class PaginationComponent implements OnInit {
    @Input() totalPages: number = 1;
    @Input() currentPage: number = 1;

    @Output() ChangePageClick: EventEmitter<String> = new EventEmitter<string>();
    pages: number[] = [];

    constructor() { }

    ngOnInit() {
        this.GeneratePages();
    }

    ChangePage(Page) {
        this.ChangePageClick.emit(Page);
    }

    ngOnChanges(changes: SimpleChanges) {
        const total: SimpleChange = changes.totalPages;
        if (total) {
            this.totalPages = total.currentValue;
        }

        const current: SimpleChange = changes.currentPage;
        if (current) {
            this.currentPage = current.currentValue;
        }

        this.GeneratePages();
    }

    GeneratePages() {
        this.pages = [];
        var startNumber = 1;
        if (this.currentPage > 1) {
            startNumber = this.currentPage - 1;
        }
        var endNumber = startNumber + 2;
        if (endNumber > this.totalPages) {
            endNumber = this.totalPages;
        }

        for (var i = startNumber; i <= endNumber; i++) {
            this.pages.push(i);
        }
    }
}
