import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from '.';

export const selectAuthState = createFeatureSelector<AuthState>('auth');

export const currentUser = createSelector(selectAuthState, auth => auth.currentUser);
export const token = createSelector(selectAuthState, auth => auth.token);
export const decodedToken = createSelector(selectAuthState, auth => auth.decodedToken);
