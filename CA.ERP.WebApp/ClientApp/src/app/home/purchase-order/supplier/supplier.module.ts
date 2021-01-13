import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupplierSelectionModalComponent } from './supplier-selection-modal/supplier-selection-modal.component';
import { SupplierEntryComponent } from './supplier-entry/supplier-entry.component';
import { SupplierService } from './services/supplier.service';
import { BrandService } from './services/brand.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';



@NgModule({
  declarations: [SupplierSelectionModalComponent, SupplierEntryComponent],
  imports: [CommonModule],
  exports: [],
  providers: [SupplierService, BrandService],
})
export class SupplierModule {}
