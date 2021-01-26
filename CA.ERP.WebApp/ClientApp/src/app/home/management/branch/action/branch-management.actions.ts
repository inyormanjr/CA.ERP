import { createAction, props } from '@ngrx/store';
import { PaginationParams } from 'src/app/models/pagination.params';
import { BranchView } from '../model/branch.view';
import { PaginationResult } from 'src/app/models/data.pagination';

export const fetchBranches = createAction(
  '[BranchManagement] Fetch branches from api'
);

export const loadBranchViewList = createAction(
  '[BranchManagement] Load Branch List',
  props<{branchViewList: BranchView[]}>()
);


export const fetchBranchPaginationResult = createAction(
  '[BranchManagement] Fetch branch pagination result',
  props<{ params: PaginationParams }>()
  );

  export const loadBranchesViewListPaginationResult = createAction(
    '[BranchManagement] Load paginated branch list',
    props<{branchViewListPaginationResult : PaginationResult<BranchView[]>}>()
  );

export const fetchingBranches = createAction(
  '[BranchManagement] Fetching branches from api'
);

export const loadBranchManagementsSuccess = createAction(
  '[BranchManagement] Load BranchManagements Success',
  props<{ data: any }>()
);

export const loadBranchManagementsFailure = createAction(
  '[BranchManagement] Load BranchManagements Failure',
  props<{ error: any }>()
);
