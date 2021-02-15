import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { NewRequest } from 'src/app/models/NewRequest';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { PoActionTypes } from '../../actions/po.actions.selector';
import { PurchaseOrderState } from '../../reducers';
import { BrandEntryComponent } from '../brand-entry/brand-entry.component';
import { Brand } from '../models/brand';
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
  bsModalRef: BsModalRef;
  constructor(private fb: FormBuilder, public bsModaRef: BsModalRef,
    private brandService: BrandService,
    private supplierService: SupplierService,
    private alertify: AlertifyService,
    private poStore: Store<PurchaseOrderState>,
    private modalService: BsModalService) {
    this.brands$ = this.brandService.get();
    this.supplierForm = this.fb.group({
      id: [],
      name: ['', Validators.required],
      address: ['', Validators.required],
      contactPerson: ['', Validators.required],
      supplierBrands: this.fb.array([], Validators.required)
    });
  }

   addBrandForm(): FormGroup {
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
       this.addBrandForm()
     );
  }
  addBrandFormParams(data : any) : FormGroup{
    return this.fb.group({
      brandId: [data.response.id, Validators.required],
      name: [data.brandEntry.data.name, Validators.required],
      description: [data.brandEntry.data.description, Validators.required],
    });
  }

  removeBrand(index) {
    this.supplierBrandsArray.removeAt(index);
  }

  createNewBrandModal(){
    this.bsModalRef = this.modalService.show(BrandEntryComponent,{
      backdrop: true,
      ignoreBackdropClick: false,
    });

    this.bsModalRef.content.event.subscribe(res => {
      this.brands$ = this.brandService.get();
      this.supplierBrandsArray.push(
        this.addBrandFormParams(res)
      );
     
    });
 
  }

  saveNewSupplier() {
    const confirm = this.alertify.confirm('Save new supplier?', () => {
      const newRequest: NewRequest = { data: this.supplierForm.value};
      this.supplierService.create(newRequest).subscribe(result => {
      this.alertify.message('New Supplier added to the database.');
      this.poStore.dispatch(
        PoActionTypes.fetchBrandsWithMasterproductsOfSupplier({
          supplieView: result,
        })
      );
        this.bsModaRef.hide();
      });
    });
  }
  ngOnInit(): void {
  }

}
