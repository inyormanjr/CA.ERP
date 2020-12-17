import { Component, OnInit } from '@angular/core';
import { UserLogin } from '../models/UserAgg/user.login';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-view',
  templateUrl: './login-view.component.html',
  styleUrls: ['./login-view.component.css']
})
export class LoginViewComponent implements OnInit {
  userLogin: UserLogin;
  constructor(private authService: AuthService) {
    this.userLogin = {username: '', password: ''};
  }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.userLogin).subscribe(response => {

    }, error => {
        console.log(error.error.title);
    });
  }

}
