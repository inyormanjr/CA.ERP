import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { BranchService } from '../../branch.service';
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
  constructor(private fB: FormBuilder, private branchService: BranchService, private store: Store<BranchManagementState>) {
    this.branchFormEntry = fB.group({
      name: ['', Validators.required],
      branchNo: ['', Validators.required],
      code: ['', Validators.required],
      address: ['', Validators.required],
      contact: ['', Validators.required]
    });
  }

  ngOnInit() {
  }

  createBranch() {
    const newBranch = Object.assign([], { branch: this.branchFormEntry.value });

    this.branchService.create(newBranch).subscribe(response => {
      console.log(response);
      this.store.dispatch(BranchManagementActions.fetchBranches());
      this.branchFormEntry.reset();
    }, error => {
        console.log(error);
    });
  }

}
