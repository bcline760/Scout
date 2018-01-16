import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeComponent } from './home.components';
import { HomeDisplayComponent } from './home.components';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
    imports: [
        CommonModule,
        HomeRoutingModule
    ],
    declarations: [
        HomeComponent,
        HomeDisplayComponent
    ],
    providers: [
        
    ]
})
export class HomeModule {
}
