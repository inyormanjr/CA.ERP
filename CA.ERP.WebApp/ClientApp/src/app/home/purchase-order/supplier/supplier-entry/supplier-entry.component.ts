import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { NewRequest } from 'src/app/models/NewRequest';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { PoActionTypes } from '../../actions/po.actions.selector';
import { PurchaseOrderState } from '../../reducers';
import { Brand } from '../models/brand';
import { BrandWithMasterProducts } from '../models/brandWithMasterProducts';
import { BrandService } from '../services/brand.service';
import { SupplierService } from '../services/supplier.service';
@Component({
  selector: 'app-supplier-entry',
  templateUrl: './supplier-entry.component.html',
  styleUrls: ['./supplier-entry.component.css']
})
export class SupplierEntryComponent implements OnInit {
  supplierForm: FormGroup;
  brands$: Observable<Brand[]>;
  selectedBrand: any = 0;

  constructor(private fb: FormBuilder, public bsModaRef: BsModalRef,
    private brandService: BrandService,
    private supplierService: SupplierService,
    private alertify: AlertifyService,
    private poStore: Store<PurchaseOrderState>) {
    this.brands$ = this.brandService.get();
    this.supplierForm = this.fb.group({
      id: [],
      name: ['', Validators.required],
      address: ['', Validators.required],
      contactPerson: ['', Validators.required],
      supplierBrands: this.fb.array([], Validators.required)
    });
  }

   createBrandForm(): FormGroup {
    return this.fb.group({
      brandId: [this.selectedBrand.id, Validators.required],
      name: [this.selectedBrand.name, Validators.required],
      description: [this.selectedBrand.description, Validators.required],
    });
  }

  get supplierBrandsArray(): FormArray {
    return this.supplierForm.controls.supplierBrands as FormArray;
  }

  addBrand() {
     this.supplierBrandsArray.push(
       this.createBrandForm()
     );
  }

  removeBrand(index) {
    this.supplierBrandsArray.removeAt(index);
  }


  saveNewSupplier() {
    const confirm = this.alertify.confirm('Save new supplier?', () => {
      const newRequest: NewRequest = { data: this.supplierForm.value};
      this.supplierService.create(newRequest).subscribe(result => {
        console.log(result);

        const useSupplier = this.alertify.confirm('New Supplier Created. Do you want to use this supplier?', () => {
          this.supplierService.getById(result.id).subscribe(supplier => {
            console.log(supplier);
            this.poStore.dispatch(PoActionTypes.selectSupplierForPurchaseOrder({ selectedSupplier: supplier }));
          }, error => { console.log(error); });
        });
        this.bsModaRef.hide();
      });
    });
  }
  ngOnInit(): void {
  }

}
