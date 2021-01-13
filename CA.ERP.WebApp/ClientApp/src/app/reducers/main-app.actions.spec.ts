import * as fromMainApp from './main-app.actions';

describe('loadMainApps', () => {
  it('should return an action', () => {
    expect(fromMainApp.loadMainApps().type).toBe('[MainApp] Load MainApps');
  });
});
