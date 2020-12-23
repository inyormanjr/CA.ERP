import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { User } from 'oidc-client';
import { environment } from '../../environments/environment';

export const authFeatureKey = 'auth';

export interface AuthState {
  currentUser: User;
}

export const reducers: ActionReducerMap<AuthState> = {
   currentUser: undefined
};


export const metaReducers: MetaReducer<AuthState>[] = !environment.production ? [] : [];
