import { createFeatureSelector, createSelector } from '@ngrx/store';
import { mainAppFeatureKey, MainAppState } from './main-app-reducer';

export const selectMainAppState = createFeatureSelector<MainAppState>(mainAppFeatureKey);


export const mainAppIsLoading = createSelector(selectMainAppState, app => app.isLoading);

export const mainAppLoadingValue = createSelector(selectMainAppState, app => app.loadingValue);
