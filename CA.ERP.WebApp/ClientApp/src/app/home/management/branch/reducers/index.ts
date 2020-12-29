import { createReducer, on } from '@ngrx/store';
import {
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../../../../environments/environment';
import { BranchView } from '../model/branch.view';
import { BranchManagementActions } from './branch.actions';

export const FeatureKey = 'branch-management';

export interface BranchManagementState {
  isLoading: boolean;
  branchesViewList: BranchView[];
  fetchSuccess: boolean;
}

export const branchManagamentInitialState: BranchManagementState = {
  isLoading: false,
  branchesViewList: [],
  fetchSuccess: undefined
}

export const reducers = createReducer(
  branchManagamentInitialState,
  on(BranchManagementActions.fetchingBranches, (state, action) => {
    return {
      ...state,
      isLoading: true
    }
  }),
  on(BranchManagementActions.loadBranchViewList, (state, action) => {
    return {
      ...state,
      isLoading: false,
      branchesViewList: action.branchViewList,
      fetchSuccess: true
    };
  }),
  on(BranchManagementActions.loadBranchManagementsFailure, (state, action) => {
    return {
      ...state,
      isLoading: false,
      fetchSuccess: false
    };
  }),
  on(BranchManagementActions.loadBranchManagementsSuccess, (state) => {
    return {
      ...state,
      isLoading: false,
      fetchSuccess: true
    }
  })
);


export const metaReducers: MetaReducer<BranchManagementState>[] = !environment.production ? [] : [];
