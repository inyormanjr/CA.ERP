import { TestBed } from '@angular/core/testing';

import { ServiceBaseService } from './service-base.service';

describe('ServiceBaseService', () => {
  let service: ServiceBaseService<any>;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceBaseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
