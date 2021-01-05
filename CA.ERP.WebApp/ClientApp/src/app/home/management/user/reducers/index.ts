import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createReducer,
  createSelector,
  MetaReducer,
  on
} from '@ngrx/store';
import { environment } from '../../../../../environments/environment';
import {fetchUsers , loadUserViewList,fetchingUsers} from '../action/user-management.actions';
import { UserView } from '../model/user.view';

export const userManagementFeatureKey = 'user-management';

export interface UserManagementState {
  isLoading : boolean;
  fetchSuccess : boolean;
  usersViewList : UserView[];
}

export const userManagementInitialState : UserManagementState = {
  isLoading : false,
  usersViewList : [],
  fetchSuccess : undefined
}

export const reducers = createReducer(
  userManagementInitialState,
  on(fetchUsers,(state,action)=>{
    return {
      ...state,
      isLoading : true
    };
  }),
  on(loadUserViewList,(state,action)=>{
    return {
      ...state,
      isLoading : false,
      usersViewList : action.usersViewList,
      fetchSuccess : true
    };
  })
);

export const metaReducers: MetaReducer<UserManagementState>[] = !environment.production ? [] : [];
