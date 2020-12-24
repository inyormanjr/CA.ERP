import { Component } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { UserLogin } from '../models/UserAgg/user.login';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  currentUser$: UserLogin;
  constructor() {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
