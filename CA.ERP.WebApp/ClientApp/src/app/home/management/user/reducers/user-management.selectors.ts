import { createFeatureSelector, createSelector } from '@ngrx/store';
import { userManagementFeatureKey, UserManagementState } from '.';

export const userManagementStateSelector = createFeatureSelector<UserManagementState>(userManagementFeatureKey);

export const isLoading = createSelector(userManagementStateSelector,
    app => app.isLoading);

export const usersViewList = createSelector(userManagementStateSelector,
    app=>app.usersViewList);

    export const fetchSuccess = createSelector(
        userManagementStateSelector,
        app => app.fetchSuccess
      );
      