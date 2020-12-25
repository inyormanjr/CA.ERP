import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AuthState } from '../auth/reducers';
import { currentUser, decodedToken } from '../auth/reducers/auth.selectors';
import { UserLogin } from '../models/UserAgg/user.login';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{
  isExpanded = false;
  currentUser$: Observable<any>;
  decodedToken$: Observable<any>;
  constructor(private authStore: Store<AuthState>) {

  }
  ngOnInit(): void {
     this.currentUser$ = this.authStore.pipe(select(currentUser));
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
