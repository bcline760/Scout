import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables';

import { PlayerService } from './player.service';
import { PlayerComponent } from './player.component';
import { PlayerListComponent } from './player-list.component';
import { PlayerDetailComponent } from './player-detail.component';
import { PlayerSalaryComponent } from './player-salary.component';
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
