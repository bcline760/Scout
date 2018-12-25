import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { PlayerAdminComponent } from './player-admin/player-admin.component';
import { ScoutAdminComponent } from './scout-admin/scout-admin.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { AdminComponent } from './admin.component';

@NgModule({
  declarations: [PlayerAdminComponent, ScoutAdminComponent, AdminViewComponent, AdminComponent],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
