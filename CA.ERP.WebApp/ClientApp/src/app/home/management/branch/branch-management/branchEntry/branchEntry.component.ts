import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { BranchService } from '../../branch.service';
import { BranchView } from '../../model/branch.view';
import { UpdateBranchRequest } from '../../model/update.branch';
import { BranchManagementState } from '../../reducers';
import { BranchManagementActions } from '../../reducers/branch.actions';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'app-branchEntry',
  templateUrl: './branchEntry.component.html',
  styleUrls: ['./branchEntry.component.scss']
})
export class BranchEntryComponent implements OnInit {
  branchFormEntry: FormGroup;
  selectedBranch$: Observable<BranchView>;
  constructor(private fB: FormBuilder,
    private branchService: BranchService,
    private store: Store<BranchManagementState>,
    private activatedRoute: ActivatedRoute,
    private alertify: AlertifyService,
  private location: Location) {
    this.branchFormEntry = fB.group({
      id: [''],
      name: ['', Validators.required],
      branchNo: [0, Validators.required],
      code: ['', Validators.required],
      address: ['', Validators.required],
      contact: ['', Validators.required]
    });
    this.activatedRoute.params.subscribe(params => {
      if (params.id !== undefined) {
           activatedRoute.data.subscribe((data) => {
             if (data !== undefined) {
               this.branchFormEntry.patchValue(data.data);
             }
           });
      }
    });

  }

  ngOnInit() {
  }

  createBranch(): void {
    this.branchService.create(this.branchFormEntry.value).subscribe(response => {
      this.alertify.message('Branch Created');
      this.store.dispatch(BranchManagementActions.fetchBranches());
      this.branchFormEntry.reset();
    }, error => {
        this.alertify.error(error.error);
    });
  }

  backPage() {
    this.location.back();
  }

  updateBranch(): void {
    const updateBranch = { data: this.branchFormEntry.value};

    console.log(updateBranch);
      this.branchService
        .update(this.branchFormEntry.value.id, updateBranch)
        .subscribe(
          (response) => {
            this.alertify.message('Branch Updated');
          },
          (error) => {
            this.alertify.error(error.error);
          }
        );
    }
}
