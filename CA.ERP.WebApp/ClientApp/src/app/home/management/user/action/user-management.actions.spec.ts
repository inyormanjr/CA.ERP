import * as fromUserManagement from './user-management.actions';

describe('loadUserManagements', () => {
  it('should return an action', () => {
    expect(fromUserManagement.loadUserManagements().type).toBe('[UserManagement] Load UserManagements');
  });
});
