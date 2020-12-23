import { createAction, props } from '@ngrx/store';
import { User } from 'oidc-client';

export const loadAuths = createAction(
  '[Auth] Load Auths'
);

export const login = createAction('[Login Page] login User',

  props <{currentUser: User}>()
);

export const logOut = createAction('[Top Navmenu] LogOut');

export const loadAuthsSuccess = createAction(
  '[Auth] Load Auths Success',
  props<{ data: any }>()
);

export const loadAuthsFailure = createAction(
  '[Auth] Load Auths Failure',
  props<{ error: any }>()
);
