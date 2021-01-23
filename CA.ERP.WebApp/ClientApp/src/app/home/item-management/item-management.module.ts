import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ItemManagementRoutingModule } from './item-management-routing.module';
import { ItemManagementViewComponent } from './item-management-view/item-management-view.component';
import { ItemListComponent } from './item-management-view/item-list/item-list.component';
import { ItemEntryComponent } from './item-management-view/item-entry/item-entry.component';
import { EffectsModule } from '@ngrx/effects';
import { ItemManageEffectEffects } from './effect/item-manage-effect.effects';


@NgModule({
  declarations: [ItemManagementViewComponent, ItemListComponent, ItemEntryComponent],
  imports: [
    CommonModule,
    ItemManagementRoutingModule,
    EffectsModule.forFeature([ItemManageEffectEffects])
  ]
})
export class ItemManagementModule { }
