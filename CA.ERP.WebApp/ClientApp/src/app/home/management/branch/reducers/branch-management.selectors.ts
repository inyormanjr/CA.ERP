import { createFeatureSelector, createSelector } from '@ngrx/store';
import { BranchManagementState, FeatureKey } from '.';

export const branchManageStateSelector = createFeatureSelector<BranchManagementState>(FeatureKey);

export const isLoading = createSelector(branchManageStateSelector, app => app.isLoading);
export const branchViewList = createSelector(branchManageStateSelector, app => app.branchesViewList);
export const fetchSuccess = createSelector(
  branchManageStateSelector,
  (app) => app.fetchSuccess
);
