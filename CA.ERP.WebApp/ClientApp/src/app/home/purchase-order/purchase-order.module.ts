import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PurchaseOrderRoutingModule } from './purchase-order-routing.module';
import { PurchaseOrderViewComponent } from './purchase-order-view/purchase-order-view.component';
import { StoreModule } from '@ngrx/store';
import * as from from './reducers';
import { EffectsModule } from '@ngrx/effects';
import { PurchaseOrderEffects } from './effects/purchase-order.effects';
import { PoEntryComponent } from './po-entry/po-entry.component';
import { PoListComponent } from './po-list/po-list.component';
import { NgxBootstrapModulesModule } from 'src/app/ngx-bootstrap-modules/ngx-bootstrap-modules.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [PurchaseOrderViewComponent, PoEntryComponent, PoListComponent],
  imports: [
    CommonModule,
    PurchaseOrderRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgxBootstrapModulesModule,
    StoreModule.forFeature(from.FeatureKey, from.reducers, { metaReducers: from.metaReducers }),
    EffectsModule.forFeature([PurchaseOrderEffects])
  ]
})
export class PurchaseOrderModule { }
