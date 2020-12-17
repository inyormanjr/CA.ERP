import { Action, createReducer, on } from '@ngrx/store';


export const mainAppFeatureKey = 'mainApp';

export interface State {
  isLoading: boolean;
}

export const initialState: State = {
  isLoading: false
};


export const reducer = createReducer(
  initialState,

);

