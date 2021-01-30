import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import * as From from './reducer/item-management-reducer.reducer';
import { ItemManagementRoutingModule } from './item-management-routing.module';
import { ItemManagementViewComponent } from './item-management-view/item-management-view.component';
import { ItemListComponent } from './item-management-view/item-list/item-list.component';
import { ItemEntryComponent } from './item-management-view/item-entry/item-entry.component';
import { EffectsModule } from '@ngrx/effects';
import { ItemManageEffectEffects } from './effect/item-manage-effect.effects';
import { StoreModule } from '@ngrx/store';
import { NgxBootstrapModulesModule } from 'src/app/ngx-bootstrap-modules/ngx-bootstrap-modules.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [ItemManagementViewComponent, ItemListComponent, ItemEntryComponent],
  imports: [
    CommonModule,
    ItemManagementRoutingModule,
    ReactiveFormsModule,
    NgxBootstrapModulesModule,
    StoreModule.forFeature(From.itemManagementReducerFeatureKey, From.reducer, {}),
    EffectsModule.forFeature([ItemManageEffectEffects])
  ]
})
export class ItemManagementModule { }
