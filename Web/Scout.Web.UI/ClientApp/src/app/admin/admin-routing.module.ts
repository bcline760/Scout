import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PlayerAdminComponent } from './player-admin/player-admin.component';
import { ScoutAdminComponent } from './scout-admin/scout-admin.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { AdminComponent } from './admin.component';
import { AuthGuard } from '../auth/auth.guard';

const routes: Routes = [
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        children: [
          { path: 'players', component: PlayerAdminComponent },
          { path: 'scouting', component: ScoutAdminComponent },
          { path: '', component: AdminViewComponent }
        ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
