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

  it('should decode token', () => {
    const service: AuthService = TestBed.get(AuthService);
    localStorage.setItem(
      'token',
      'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI5MTU2NzBlZS03Nzc0LTQ5MTgtOWZjZC01ODY5MjM2NTU4MWQiLCJSb2xlSW50IjoiMSIsIlVzZXJuYW1lIjoiQWRtaW4iLCJGaXJzdE5hbWUiOiJEb21lbmljYSIsIkxhc3ROYW1lIjoiV29sZmYiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2MDg4NDE3NjksImV4cCI6MTYxMTQzMzc2OSwiaWF0IjoxNjA4ODQxNzY5fQ.ED0sQWSpF961MbGe9DOqfCPtMKp4t4ucyj12-jt6ApfTc8sMbCxtTkyf3VZngYkqagniVsnBsVsgzy_4zwTqlw'
    );

    expect(service.decodedToken()).toBeDefined();
  });
});


