import { Component } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { AuthState } from '../auth/reducers';
import { UserLogin } from '../models/UserAgg/user.login';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  currentUser$: UserLogin;
  constructor(private authStore: Store<AuthState>) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
