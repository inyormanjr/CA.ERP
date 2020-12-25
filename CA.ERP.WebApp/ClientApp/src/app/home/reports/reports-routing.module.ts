import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReportsViewComponent } from './reports-view/reports-view.component';


const routes: Routes = [{path: '', component: ReportsViewComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
