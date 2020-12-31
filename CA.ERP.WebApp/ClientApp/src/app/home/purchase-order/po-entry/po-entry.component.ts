import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { BranchService } from '../../management/branch/branch.service';
import { BranchView } from '../../management/branch/model/branch.view';

@Component({
  selector: 'app-po-entry',
  templateUrl: './po-entry.component.html',
  styleUrls: ['./po-entry.component.scss'],
})
export class PoEntryComponent implements OnInit {
  branches$: Observable<BranchView[]>;

  poForm: FormGroup;
  constructor(private branchService: BranchService, private fb: FormBuilder) {
    this.poForm = this.fb.group({
      supplierId: ['', [Validators.required]],
      branchId: ['', [Validators.required]],
      poDate: ['', [Validators.required]],
      deliveryDate: ['', [Validators.required]],
      details: [this.createDetails]
    });
  }

  get createDetails(): FormGroup {
    return this.fb.group({
      brand: ['test'],
      model: ['fasfas'],
      orderedQty: [2],
      freeQty: [2],
      cost: [200],
      discount: [50],
      subTotal: [222]
    });
  }

  ngOnInit(): void {
    this.branches$ = this.branchService.get();
  }
}
