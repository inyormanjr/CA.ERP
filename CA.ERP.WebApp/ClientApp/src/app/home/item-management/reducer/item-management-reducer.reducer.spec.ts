import { reducer, stockManageState } from './item-management-reducer.reducer';

describe('ItemManagementReducer Reducer', () => {
  describe('an unknown action', () => {
    it('should return the previous state', () => {
      const action = {} as any;

      const result = reducer(stockManageState, action);

      expect(result).toBe(stockManageState);
    });
  });
});
