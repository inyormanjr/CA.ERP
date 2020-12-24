import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthState } from '../auth/reducers';
import { ERP_Auth_Actions } from '../auth/reducers/auth.action.types';
import { UserLogin } from '../models/UserAgg/user.login';
import { MainAppState } from '../reducers/main-app-reducer';
import { ERP_Main_Actions } from '../reducers/main.action.types';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-view',
  templateUrl: './login-view.component.html',
  styleUrls: ['./login-view.component.css'],
})
export class LoginViewComponent implements OnInit {
  userLogin: UserLogin;
  isLoading = false;
  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private authState: Store<AuthState>,
    private mainAppState: Store<MainAppState>,
    private router: Router
  ) {
    this.userLogin = { username: '', password: '' };

    this.userLoginForm = fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }
  userLoginForm: FormGroup;
  ngOnInit() {}

  login() {
    const userCredentials = this.userLoginForm.value;
    this.isLoading = true;
    this.authService.login(userCredentials).subscribe(
      (response) => {
       this.isLoading = false;
        this.authState.dispatch(ERP_Auth_Actions.login({
          token: localStorage.getItem('token')
        }));
        this.router.navigateByUrl('home');
      },
      (error) => {
        this.isLoading = false;
        console.log(error.error.title);
      }
    );
  }
}
