import { TestBed } from '@angular/core/testing';

import { HttpIntercepterService } from './http-intercepter.service';

describe('HttpIntercepterService', () => {
  let service: HttpIntercepterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HttpIntercepterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
