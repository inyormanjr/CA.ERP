import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ItemManagementRoutingModule } from './item-management-routing.module';
import { ItemManagementViewComponent } from './item-management-view/item-management-view.component';


@NgModule({
  declarations: [ItemManagementViewComponent],
  imports: [
    CommonModule,
    ItemManagementRoutingModule
  ]
})
export class ItemManagementModule { }
