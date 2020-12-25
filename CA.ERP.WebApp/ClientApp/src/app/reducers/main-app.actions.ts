import { createAction, props } from '@ngrx/store';


export const loadingMainApp = createAction(
  '[MainApp] App is loading',
);

export const loadMainApps = createAction(
  '[MainApp] Load MainApps'
);

export const loadMainAppsSuccess = createAction(
  '[MainApp] Load MainApps Success',
);

export const loadMainAppsFailure = createAction(
  '[MainApp] Load MainApps Failure',
  props<{ error: any }>()
);