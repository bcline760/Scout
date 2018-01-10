import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ErrorComponent } from './error/error.component';
import { PlayersComponent } from './players/players.component';
import { PlayerComponent } from './player/player.component';
import { PlayerResolverService } from './player/player-resolver.service';
import { TeamsComponent } from './teams/teams.component';
import { TeamComponent } from './team/team.component';

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'error', component: ErrorComponent },
    { path: 'players', component: PlayersComponent },
    {
        path: 'players/:letter', component: PlayersComponent, resolve: {
            player: PlayerResolverService
        }
    },
    {
        path: 'player/:id', component: PlayerComponent, resolve: {
            player: PlayerResolverService
        }
    },
    { path: 'teams', component: TeamsComponent },
    { path: 'team/:id', component: TeamComponent },
    { path: 'team/:id/:yr', component: TeamComponent }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [RouterModule],
    providers: [
        PlayerResolverService
    ]
})
export class AppRoutingModule { }
