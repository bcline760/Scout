import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { LoadingModule } from 'ngx-loading';

import { PlayerModule } from './player/player.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { TeamsComponent } from './teams/teams.component';
import { TeamComponent } from './team/team.component';
import { AppRoutingModule } from './/app-routing.module';
import { ErrorComponent } from './error/error.component';


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        TeamsComponent,
        TeamComponent,
        ErrorComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        LoadingModule,
        PlayerModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
