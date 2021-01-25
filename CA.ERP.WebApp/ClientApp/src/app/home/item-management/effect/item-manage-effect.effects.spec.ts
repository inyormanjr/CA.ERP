import { TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { Observable } from 'rxjs';

import { ItemManageEffectEffects } from './item-manage-effect.effects';

describe('ItemManageEffectEffects', () => {
  let actions$: Observable<any>;
  let effects: ItemManageEffectEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ItemManageEffectEffects,
        provideMockActions(() => actions$)
      ]
    });

    effects = TestBed.inject(ItemManageEffectEffects);
  });

  it('should be created', () => {
    expect(effects).toBeTruthy();
  });
});
