import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ItemEntryComponent } from './item-management-view/item-entry/item-entry.component';
import { ItemListComponent } from './item-management-view/item-list/item-list.component';
import { ItemManagementViewComponent } from './item-management-view/item-management-view.component';
import { StocklistResolverService } from './resolvers/stocklist-resolver.service';


const routes: Routes = [
  {
    path: '',
    component: ItemManagementViewComponent,
    children: [
      {
        path: 'list',
        component: ItemListComponent,
        resolve: { data: StocklistResolverService },
      },
      { path: 'entry', component: ItemEntryComponent },
      { path: '', redirectTo: 'list' },
      { path: '**', redirectTo: 'list', pathMatch: 'full' },
    ],
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ItemManagementRoutingModule { }
