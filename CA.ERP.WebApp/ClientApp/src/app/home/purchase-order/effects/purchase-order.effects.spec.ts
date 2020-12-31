import { TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { Observable } from 'rxjs';

import { PurchaseOrderEffects } from './purchase-order.effects';

describe('PurchaseOrderEffects', () => {
  let actions$: Observable<any>;
  let effects: PurchaseOrderEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        PurchaseOrderEffects,
        provideMockActions(() => actions$)
      ]
    });

    effects = TestBed.inject(PurchaseOrderEffects);
  });

  it('should be created', () => {
    expect(effects).toBeTruthy();
  });
});
