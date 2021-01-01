import { TestBed } from '@angular/core/testing';

import { Po.List.ResolverService } from './po.list.resolver.service';

describe('Po.List.ResolverService', () => {
  let service: Po.List.ResolverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Po.List.ResolverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
