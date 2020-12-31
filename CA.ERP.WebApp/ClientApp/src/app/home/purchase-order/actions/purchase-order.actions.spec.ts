import * as fromPurchaseOrder from './purchase-order.actions';

describe('loadPurchaseOrders', () => {
  it('should return an action', () => {
    expect(fromPurchaseOrder.loadPurchaseOrders().type).toBe('[PurchaseOrder] Load PurchaseOrders');
  });
});
