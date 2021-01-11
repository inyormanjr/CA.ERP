import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { UserView } from '../../model/user.view';
import { UserManagementState } from '../../reducers';
import { UserManagementSelectorType } from '../../reducers/user-management.selectors.type';
import { fetchUsers } from '../../action/user-management.actions';
import { Role } from '../../model/user.role';
@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  isLoading$: Observable<boolean>
  fetchSuccess$: Observable<boolean>;
  userViewList$: Observable<UserView[]>;
  constructor(
    private store: Store<UserManagementState>
  ) { }

  ngOnInit(): void {
    this.userViewList$ = this.store.pipe(select(UserManagementSelectorType.usersViewList));
    this.isLoading$ = this.store.pipe(select(UserManagementSelectorType.isLoading));
    this.fetchSuccess$ = this.store.pipe(select(UserManagementSelectorType.fetchSuccess));
    this.store.dispatch(fetchUsers());
  }

  selectedRole(rolesFlag: number) {
    var selectedRoles = [];
    for (var enumFlag in Role) {

      var val = Number(Role[enumFlag]);

      if ((rolesFlag & val) === val) {
        selectedRoles.push(enumFlag);
      }

    }

    return selectedRoles;
  }

}
