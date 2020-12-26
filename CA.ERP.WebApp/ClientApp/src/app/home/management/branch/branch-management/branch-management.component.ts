import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { fetchBranches } from '../action/branch-management.actions';
import { BranchService } from '../branch.service';
import { BranchView } from '../model/branch.view';
import { BranchManagementState } from '../reducers';
import { branchViewList } from '../reducers/branch-management.selectors';

@Component({
  selector: 'app-branch-management',
  templateUrl: './branch-management.component.html',
  styleUrls: ['./branch-management.component.css']
})
export class BranchManagementComponent implements OnInit {
  branchViewList$: Observable<BranchView[]>;
  constructor(private store: Store<BranchManagementState>, private service: BranchService) { }

  ngOnInit(): void {
    this.branchViewList$ = this.store.pipe(select(branchViewList));
    this.store.dispatch(fetchBranches());
  }

}
