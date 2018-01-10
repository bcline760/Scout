import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { LoadingModule } from 'ngx-loading';
import { DataTablesModule } from 'angular-datatables';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { PlayersComponent } from './players/players.component';
import { PlayerComponent } from './player/player.component';
import { TeamsComponent } from './teams/teams.component';
import { TeamComponent } from './team/team.component';
import { AppRoutingModule } from './/app-routing.module';
import { PlayerService } from './player.service';
import { ErrorComponent } from './error/error.component';


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        PlayersComponent,
        PlayerComponent,
        TeamsComponent,
        TeamComponent,
        ErrorComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        LoadingModule,
        DataTablesModule
    ],
    providers: [PlayerService],
    bootstrap: [AppComponent]
})
export class AppModule { }
