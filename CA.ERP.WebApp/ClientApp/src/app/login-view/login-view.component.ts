import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AuthState } from '../auth/reducers';
import { UserLogin } from '../models/UserAgg/user.login';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-view',
  templateUrl: './login-view.component.html',
  styleUrls: ['./login-view.component.css'],
})
export class LoginViewComponent implements OnInit {
  userLogin: UserLogin;
  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private authState: Store<AuthState>
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
    const userCredentials = Object.assign([], this.userLoginForm.value);
    this.authService.login(userCredentials).subscribe(
      (response) => {},
      (error) => {
        console.log(error.error.title);
      }
    );
  }
}
