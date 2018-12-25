import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { PlayerRoutingModule } from './player-routing.module';
import { PlayerViewComponent } from './player-view/player-view.component';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { PlayerSearchComponent } from './player-search/player-search.component';

@NgModule({
  declarations: [PlayerViewComponent, PlayerDetailComponent, PlayerSearchComponent],
  imports: [
    CommonModule,
    FormsModule,
    PlayerRoutingModule
  ]
})
export class PlayerModule { }
