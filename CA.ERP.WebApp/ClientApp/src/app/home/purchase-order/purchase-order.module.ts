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
import { SupplierModule } from './supplier/supplier.module';
import { SupplierSelectionModalComponent } from './supplier/supplier-selection-modal/supplier-selection-modal.component';
import { FilterPipeModule } from 'ngx-filter-pipe';
import { PurchaseOrderPdfViewerComponent } from './purchase-order-pdf-viewer/purchase-order-pdf-viewer.component';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { SupplierEntryComponent } from './supplier/supplier-entry/supplier-entry.component';
import { BrandEntryComponent } from './supplier/brand-entry/brand-entry.component';


@NgModule({
  declarations: [
    PurchaseOrderViewComponent,
    PoEntryComponent,
    PoListComponent,
    SupplierSelectionModalComponent,
    SupplierEntryComponent,
    PurchaseOrderPdfViewerComponent,
    BrandEntryComponent
  ],
  imports: [
    CommonModule,
    PurchaseOrderRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    PdfViewerModule,
    NgxBootstrapModulesModule,
    FilterPipeModule,
    StoreModule.forFeature(from.FeatureKey, from.reducers, {
      metaReducers: from.metaReducers,
    }),
    EffectsModule.forFeature([PurchaseOrderEffects]),
  ],
})
export class PurchaseOrderModule {}
