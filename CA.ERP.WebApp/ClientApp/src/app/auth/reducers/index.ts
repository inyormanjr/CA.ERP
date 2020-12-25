import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createReducer,
  createSelector,
  MetaReducer,
  on
} from '@ngrx/store';
import { environment } from 'src/environments/environment';
import { ERP_Auth_Actions } from './auth.action.types';

export const authFeatureKey = 'auth';

export interface AuthState {
  currentUser: any;
  token: String;
  decodedToken: any;
}

export const initialAuthState: AuthState = {
  currentUser: undefined,
  token: '',
  decodedToken: null
};

export const reducers = createReducer(
  initialAuthState,
  on(ERP_Auth_Actions.login, (state, action) => {
    return {
      ...state,
      token: action.token,
    };
  }),
  on(ERP_Auth_Actions.attachCurrentUser, (state, action) => {
    return {
      ...state,
      currentUser: action.currentUser,
    };
  }),
  on(ERP_Auth_Actions.logOut, (state, action) => {
    return { ...state, token: undefined };
  })
);


export const metaReducers: MetaReducer<AuthState>[] = !environment.production ? [] : [];
