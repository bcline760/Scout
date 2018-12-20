import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ErrorRoutingModule } from './error-routing.module';
import { PageMissingComponent } from './page-missing/page-missing.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { ErrorComponent } from './error.component';

@NgModule({
  declarations: [PageMissingComponent, ServerErrorComponent, ErrorComponent],
  imports: [
    CommonModule,
    ErrorRoutingModule
  ]
})
export class ErrorModule { }
