import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AuthState } from '../auth/reducers';
import { currentUser, decodedToken } from '../auth/reducers/auth.selectors';
import { UserLogin } from '../models/UserAgg/user.login';
import { MainAppState } from '../reducers/main-app-reducer';
import { MainAppSelectorType } from '../reducers/main.app.selector.type';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{
  isExpanded = false;
  currentUser$: Observable<any>;
  decodedToken$: Observable<any>;
  loadinValue$: Observable<any>;
  constructor(private authStore: Store<AuthState>,
              private mainAppStore: Store<MainAppState>) {

  }
  ngOnInit(): void {
    this.currentUser$ = this.authStore.pipe(select(currentUser));
    this.loadinValue$ = this.mainAppStore.pipe(select(MainAppSelectorType.mainAppLoadingValue));
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
