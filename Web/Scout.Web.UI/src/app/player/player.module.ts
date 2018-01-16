import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables';

import { PlayerService } from './player.service';
import { PlayerComponent, PlayerListComponent, PlayerDetailComponent, PlayerSalaryComponent } from './player.components';
import { PlayerRoutingModule } from './player-routing.module';

@NgModule({
  imports: [
      CommonModule,
      PlayerRoutingModule,
      DataTablesModule
  ],
  declarations: [
      PlayerComponent,
      PlayerListComponent,
      PlayerDetailComponent,
      PlayerSalaryComponent
  ],
  providers: [
      PlayerService
  ]
})
export class PlayerModule { }
