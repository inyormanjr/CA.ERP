import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { PaginationResult } from 'src/app/models/data.pagination';
import { fetchBranches, fetchBranchPaginationResult } from '../../action/branch-management.actions';
import { BranchService } from '../../branch.service';
import { BranchView } from '../../model/branch.view';
import { BranchManagementState } from '../../reducers';
import { BranchMangementSelectorType } from '../../reducers/branch.management.selectors.type';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'app-BranchList',
  templateUrl: './BranchList.component.html',
  styleUrls: ['./BranchList.component.scss'],
})
export class BranchListComponent implements OnInit {
  isLoading$: Observable<boolean>;
  fetchSuccess$: Observable<boolean>;
  branchViewList$: Observable<BranchView[]>;
  page : 1;
  constructor(
    private store: Store<BranchManagementState>,
    private service: BranchService
  ) {}

  ngOnInit() {
    this.branchViewList$ = this.store.pipe(select(BranchMangementSelectorType.branchViewList));
    // this.isLoading$ = this.store.pipe(
    //   select(BranchMangementSelectorType.isLoading)
    // );
    this.fetchSuccess$ = this.store.pipe(select(BranchMangementSelectorType.fetchSuccess));
    this.store.dispatch(fetchBranches());

  }

}
