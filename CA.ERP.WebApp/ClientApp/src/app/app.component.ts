import { Component, OnInit } from '@angular/core';
import { Router, Event, NavigationStart } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthState } from './auth/reducers';
import { ERP_Auth_Actions } from './auth/reducers/auth.action.types';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'app';
  constructor(private store: Store<AuthState>,
    private authService: AuthService, private router: Router) {
    this.router.events.subscribe((event: Event) => {
      switch (true) {
        case event instanceof NavigationStart: {}
        }
    });
     }
  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.store.dispatch(ERP_Auth_Actions.login({ token }));
      this.store.dispatch(ERP_Auth_Actions.attachCurrentUser({ currentUser: this.authService.decodedToken() }));
    }
  }
}
