import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PlayerComponent } from './player.component';
import { PlayerViewComponent } from './player-view/player-view.component';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { PlayerSearchComponent } from './player-search/player-search.component';

const routes: Routes = [
  {
    path: 'player',
    component: PlayerComponent,
    children: [
      {
        path: '',
        component: PlayerSearchComponent,
        children: [
          {
            path: ':id',
            component: PlayerDetailComponent
          },
          {
            path: '',
            component: PlayerViewComponent
          }
        ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PlayerRoutingModule { }
