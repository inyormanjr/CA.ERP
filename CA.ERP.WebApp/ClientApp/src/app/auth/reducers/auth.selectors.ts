import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from '.';

export const selectAuthState = createFeatureSelector<AuthState>('AuthState');

export const currentUser = createSelector(selectAuthState, auth => auth.currentUser);
export const token = createSelector(selectAuthState, auth => auth.token);
