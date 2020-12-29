import { createAction, props } from '@ngrx/store';
import { BranchView } from '../model/branch.view';


export const fetchBranches = createAction(
  '[BranchManagement] Fetch branches from api'
);

export const loadBranchViewList = createAction(
  '[BranchManagement] Load Branch List',
  props<{branchViewList: BranchView[]}>()
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
