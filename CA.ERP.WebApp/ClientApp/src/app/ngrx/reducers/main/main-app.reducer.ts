import { Action, createReducer, on } from '@ngrx/store';


export const mainAppFeatureKey = 'mainApp';

export interface State {
  isLoading: boolean;
}

export const initialState: State = {
  isLoading: false
};


const _reducer = createReducer(
  initialState

);

export function reducer(initialState, action) {
  return _reducer(initialState, action);
}


