import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

import { BranchView } from './model/branch.view';
import { BranchService } from './branch.service';

describe('BranchService', () => {
  let service: BranchService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        JwtModule.forRoot({
          config: {
            whitelistedDomains: [environment.apiURL],
            blacklistedRoutes: [environment.apiURL + '/api/Authentication'],
          },
        }),
      ],
    });
    service = TestBed.inject(BranchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return branchView List objects', () => {
    expect(service.get()).toBeTruthy();
  });
});
