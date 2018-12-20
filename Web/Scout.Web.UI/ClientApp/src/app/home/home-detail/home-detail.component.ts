import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-detail',
  templateUrl: './home-detail.component.html',
  styleUrls: ['./home-detail.component.css']
})
export class HomeDetailComponent implements OnInit {

  title: string;
  constructor() {

  }

  ngOnInit() {
    this.title = 'Find your player, and scout them!';
  }

}
