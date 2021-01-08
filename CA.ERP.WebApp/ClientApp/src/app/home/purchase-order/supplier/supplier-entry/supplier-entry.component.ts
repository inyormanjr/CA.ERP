import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { Brand } from '../models/brand';
import { BrandWithMasterProducts } from '../models/brandWithMasterProducts';
import { BrandService } from '../services/brand.service';
@Component({
  selector: 'app-supplier-entry',
  templateUrl: './supplier-entry.component.html',
  styleUrls: ['./supplier-entry.component.css']
})
export class SupplierEntryComponent implements OnInit {
  supplierForm: FormGroup;
  brands$: Observable<Brand[]>;

  constructor(private fb: FormBuilder, public bsModaRef: BsModalRef, private brandService: BrandService) {
    this.brands$ = this.brandService.get();
    this.supplierForm = this.fb.group({
      id: [],
      name: ['', Validators.required],
      address: ['', Validators.required],
      contactPerson: ['', Validators.required],
      supplierBrands: this.fb.array([])
    });
  }

  get createBrandForm(): FormGroup {
    return this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  get supplierBrandsArray(): FormArray {
    return this.supplierForm.controls.supplierBrands as FormArray;
  }

  addBrand() {
    this.supplierBrandsArray.push(this.createBrandForm);
    console.log(this.supplierBrandsArray);
  }

  ngOnInit(): void {
  }

}
