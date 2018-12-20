import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeDetailComponent } from './home-detail/home-detail.component';
import { HomeComponent } from './home.component';

@NgModule({
  declarations: [HomeDetailComponent, HomeComponent],
  imports: [
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
