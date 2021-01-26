import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { UserView } from '../../model/user.view';
import { UserManagementState } from '../../reducers';
import { UserManagementSelectorType } from '../../reducers/user-management.selectors.type';
import { fetchUsers, fetchUsersPaginationResult } from '../../action/user-management.actions';
import { Role } from '../../model/user.role';
import { PaginationResult } from 'src/app/models/data.pagination';
@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  isLoading$: Observable<boolean>
  fetchSuccess$: Observable<boolean>;
  userViewListPaginationResult$: Observable<PaginationResult<UserView[]>>;
  firstName : string = "";
  lastName : string = "";

  constructor(
    private store: Store<UserManagementState>
  ) { }

  ngOnInit(): void {
    this.userViewListPaginationResult$ = this.store.pipe(select(UserManagementSelectorType.userViewListPaginationResult));
    //this.isLoading$ = this.store.pipe(select(UserManagementSelectorType.isLoading));
    this.fetchSuccess$ = this.store.pipe(select(UserManagementSelectorType.fetchSuccess));
    
    this.store.dispatch(fetchUsersPaginationResult({
      params : {
        page : 1,
        pageSize : 5,
        queryParams : []
      }
    }));
 
  }
  
  pageChange(event) {
  
    this.store.dispatch(fetchUsersPaginationResult({
      params : {
        page : event,
        pageSize : 5,
        queryParams : [
          {name : 'firstName' , value : this.firstName},
          {name : 'lastName' , value : this.lastName}
        ]
      }
    }));
  }

  search(){
    this.store.dispatch(fetchUsersPaginationResult({
      params : {
        page : 1,
        pageSize : 5,
        queryParams : [
          {name : 'firstName' , value : this.firstName},
          {name : 'lastName' , value : this.lastName}
        ]
      }
    }));
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
