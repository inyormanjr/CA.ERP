import * as fromItemManageActions from './item-manage-actions.actions';

describe('loadItemManageActionss', () => {
  it('should return an action', () => {
    expect(fromItemManageActions.loadItemManageActionss().type).toBe('[ItemManageActions] Load ItemManageActionss');
  });
});
