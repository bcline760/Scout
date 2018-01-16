import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { LoadingModule } from 'ngx-loading';

import { PlayerModule } from './player/player.module';
import { HomeModule } from './home/home.module';

import { AppComponent } from './app.component';
import { TeamsComponent } from './teams/teams.component';
import { TeamComponent } from './team/team.component';
import { AppRoutingModule } from './app-routing.module';
import { ErrorComponent } from './error/error.component';


@NgModule({
    declarations: [
        AppComponent,
        TeamsComponent,
        TeamComponent,
        ErrorComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        LoadingModule,
        HomeModule,
        PlayerModule,
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
