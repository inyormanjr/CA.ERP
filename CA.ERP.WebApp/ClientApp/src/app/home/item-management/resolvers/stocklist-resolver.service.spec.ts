import { TestBed } from '@angular/core/testing';

import { StocklistResolverService } from './stocklist-resolver.service';

describe('StocklistResolverService', () => {
  let service: StocklistResolverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StocklistResolverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
