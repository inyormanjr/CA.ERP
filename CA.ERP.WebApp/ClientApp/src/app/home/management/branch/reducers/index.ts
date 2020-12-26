import { createReducer, on } from '@ngrx/store';
import {
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../../../../environments/environment';
import { loadBranchViewList } from '../action/branch-management.actions';
import { BranchView } from '../model/branch.view';

export const FeatureKey = 'branch-management';

export interface BranchManagementState {
  isLoading: boolean;
  branchesViewList: BranchView[];
}

export const branchManagamentInitialState: BranchManagementState = {
  isLoading: false,
  branchesViewList: []
}

export const reducers = createReducer(
  branchManagamentInitialState,
  on(loadBranchViewList, (state, action) => {
    return {
      ...state,
      branchesViewList: action.branchViewList
    };
  })
);


export const metaReducers: MetaReducer<BranchManagementState>[] = !environment.production ? [] : [];
