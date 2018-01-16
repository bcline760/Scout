import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-home',
    templateUrl: './html/home.component.html',
    styleUrls: ['./css/home.component.css']
})
export class HomeComponent implements OnInit {

    constructor() { }

    ngOnInit() {
    }
}

@Component({
    selector: 'app-home-display',
    templateUrl: './html/home-display.component.html',
    styleUrls: ['./css/home-display.component.css']
})
/** home-display component*/
export class HomeDisplayComponent {
    /** home-display ctor */
    constructor() {

    }
}
