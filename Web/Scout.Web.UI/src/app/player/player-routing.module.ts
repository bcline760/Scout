import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PlayerComponent } from './player.component';
import { PlayerListComponent } from './player-list.component';
import { PlayerDetailComponent } from './player-detail.component';
import { PlayerSalaryComponent } from './player-salary.component';
import { PlayerResolverService } from './player-resolver.service';

const playerRoutes: Routes = [
    {
        path: 'player',
        component: PlayerComponent,
        children: [
            {
                path: '',
                component: PlayerListComponent
            },
            {
                path: ':id',
                component: PlayerDetailComponent,
                resolve: {
                    player: PlayerResolverService
                }
            },
            {
                path: ':id/salary',
                component: PlayerSalaryComponent
            }
        ]
    },
]
@NgModule({
    imports: [
        RouterModule.forChild(playerRoutes)
    ],
    exports:[
        RouterModule
    ],
    providers: [
        PlayerResolverService
    ]
})
export class PlayerRoutingModule { }
