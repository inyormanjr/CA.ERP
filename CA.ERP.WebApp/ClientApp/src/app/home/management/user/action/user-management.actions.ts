import { createAction, props } from '@ngrx/store';
import { UserView } from '../model/user.view';

export const fetchUsers = createAction(
  '[UserManagement] Fetch users'
);

export const fetchingUsers = createAction(
  '[UserManagement] Fetching users'
);

export const loadUserViewList = createAction(
  '[UserManagement] Load users list',
  props<{usersViewList : UserView[]}>()
);

export const loadUserManagementsSuccess = createAction(
  '[UserManagement] Load UserManagement Success',
  props<{ data: any }>()
);

export const loadUserManagementsFailure = createAction(
  '[UserManagement] Load UserManagement Failure',
  props<{ error: any }>()
);




