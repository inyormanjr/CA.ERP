import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createReducer,
  createSelector,
  MetaReducer,
  on
} from '@ngrx/store';
import { environment } from '../../environments/environment';
import { ERP_Main_Actions } from './main.action.types';

export const mainAppFeatureKey = 'mainApp';

export interface MainAppState {
  isLoading: Boolean;
}

export const initialStateMainAppState = {
  isLoading: false
};

export const mainAppReducer = createReducer(
  initialStateMainAppState,
  on(ERP_Main_Actions.loadingMainApp, (state, action) => {
    return {
      ...state,
      isLoading: true
    };
  }),
  on(ERP_Main_Actions.loadMainAppsSuccess, (state, action) => {
    return {
      ...state,
      isLoading: false
    };
  })
);



export const metaReducers: MetaReducer<MainAppState>[] = !environment.production
  ? []
  : [];
