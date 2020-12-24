import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { UserLogin } from 'src/app/models/UserAgg/user.login';
import { environment } from 'src/environments/environment';

export const authFeatureKey = 'auth';

export interface AuthState {
  currentUser: String;
  token: String;
}

export const reducers: ActionReducerMap<AuthState> = {
  currentUser: undefined,
  token: undefined
};


export const metaReducers: MetaReducer<AuthState>[] = !environment.production ? [] : [];
