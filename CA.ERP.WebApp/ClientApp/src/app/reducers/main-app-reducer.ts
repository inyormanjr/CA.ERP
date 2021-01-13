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
  loadingValue: number;
}

export const initialStateMainAppState = {
  isLoading: false,
  loadingValue: undefined
};

export const mainAppReducer = createReducer(
  initialStateMainAppState,
  on(ERP_Main_Actions.loadingMainApp, (state, action) => {
    return {
      ...state,
      isLoading: true
    };
  }),
  on(ERP_Main_Actions.updateLoadingValue, (state, action) => {
    return {
      ...state,
      loadingValue: action.value
    }
  }),
  on(ERP_Main_Actions.loadMainAppsSuccess, (state, action) => {
    return {
      ...state,
      isLoading: false,
      loadingValue: undefined
    };
  })
);



export const metaReducers: MetaReducer<MainAppState>[] = !environment.production
  ? []
  : [];
