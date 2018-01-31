import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
    selector: 'app-error',
    templateUrl: './error.component.html',
    styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {

    constructor(private location: Location) { }

    ngOnInit() {
    }

   GoBack() {
        this.location.back();
    }
}
