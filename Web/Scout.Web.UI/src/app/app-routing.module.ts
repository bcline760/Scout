import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ErrorComponent } from './error/error.component';
import { PlayerComponent } from './player/player.component';
import { TeamsComponent } from './teams/teams.component';
import { TeamComponent } from './team/team.component';

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'error', component: ErrorComponent },
    {
        path: 'player',
        loadChildren: 'app/player/player.module#PlayerModule'
    },
    { path: 'teams', component: TeamsComponent },
    { path: 'team/:id', component: TeamComponent },
    { path: 'team/:id/:yr', component: TeamComponent }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
