import { TestBed } from '@angular/core/testing';
 import {
   HttpClientTestingModule,
   HttpTestingController,
 } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { UserLogin } from '../models/UserAgg/user.login';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

describe('AuthService', () => {
  beforeEach(() =>
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
      providers: [AuthService],
    })
  );

  it('should be created', () => {
    const service: AuthService = TestBed.get(AuthService);
    expect(service).toBeTruthy();
  });


  it('should login', () => {
    const service: AuthService = TestBed.get(AuthService);
    const userLogin: UserLogin = { username: 'Admin', password: 'password' };
    expect(service.login(userLogin)).toBeTruthy();
  });
});
