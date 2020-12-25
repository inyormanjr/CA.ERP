import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { JwtModule } from '@auth0/angular-jwt';
import { StoreModule } from '@ngrx/store';
import { environment } from 'src/environments/environment';

import * as fromMainApp from './../reducers/main-app-reducer';
import { LoginViewComponent } from './login-view.component';

describe('LoginViewComponent', () => {
  let component: LoginViewComponent;
  let fixture: ComponentFixture<LoginViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        StoreModule.forRoot(
          { 'main-app': fromMainApp.mainAppReducer },
          { runtimeChecks: { strictStateSerializability: true } }
        ),
        JwtModule.forRoot({
          config: {
            whitelistedDomains: [environment.apiURL],
            blacklistedRoutes: [environment.apiURL + '/api/Authentication'],
          },
        }),
      ],
      declarations: [LoginViewComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should login', () => {
    expect(component.login()).toBe();
  });
});
