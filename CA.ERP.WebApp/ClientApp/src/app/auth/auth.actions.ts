import { createAction, props } from '@ngrx/store';
import { UserLogin } from '../models/UserAgg/user.login';

export const loadAuths = createAction(
  '[Auth] Load Auths'
);

export const login = createAction(
  '[Login Page] Authenticate User',
  props<{ userLogin: UserLogin }>()
);

export const logOut = createAction(
  '[MainNav] Log-Out User'
);

export const loadAuthsSuccess = createAction(
  '[Auth] Load Auths Success',
  props<{ data: any }>()
);

export const loadAuthsFailure = createAction(
  '[Auth] Load Auths Failure',
  props<{ error: any }>()
);
