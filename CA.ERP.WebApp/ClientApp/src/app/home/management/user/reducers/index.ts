import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../../../../environments/environment';

export const userManagementFeatureKey = 'user-management';

export interface UserManagementState {

}

export const reducers: ActionReducerMap<UserManagementState> = {

};


export const metaReducers: MetaReducer<UserManagementState>[] = !environment.production ? [] : [];
