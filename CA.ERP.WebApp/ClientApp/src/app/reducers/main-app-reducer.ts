import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createReducer,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../environments/environment';

export const mainAppFeatureKey = 'mainApp';

export interface MainAppState {
  isLoading: Boolean;
}

export const initialStateMainAppState: MainAppState = {
  isLoading: false
};

export const mainAppReducer = createReducer(
  initialStateMainAppState
);



export const metaReducers: MetaReducer<MainAppState>[] = !environment.production
  ? []
  : [];
