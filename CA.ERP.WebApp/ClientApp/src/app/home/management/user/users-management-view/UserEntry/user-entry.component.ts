import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { UserManagementActions } from '../../action/user.action';
import { UserManagementState } from '../../reducers';
import { UserService } from '../../user.service';
import { Location } from '@angular/common';
import { MustMatch } from 'src/app/misc/validators/mustMatch';
import {Role} from '../../model/user.role';
import { BranchService } from '../../../branch/branch.service';
import { Observable } from 'rxjs';
import { BranchView } from '../../../branch/model/branch.view';
@Component({
  selector: 'app-user-entry',
  templateUrl: './user-entry.component.html',
  styleUrls: ['./user-entry.component.css']
})
export class UserEntryComponent implements OnInit {

  userEntryForm : FormGroup;
  branches$ : Observable<BranchView[]>;
  
  roles = Object.values(Role).map(val => {
    return {
      name : val,
      num : Role[val]
    }
  }).splice(0,Object.keys(Role).length/2);

  constructor(private formBuilder : FormBuilder,
              private userService : UserService,
              private userStore : Store<UserManagementState>,
              private alertify : AlertifyService,
              private location : Location,
              private branchService : BranchService) {
                  this.userEntryForm = this.formBuilder.group({
                    userName:['',[Validators.required,Validators.minLength(5)]],
                    password:['',[Validators.required,Validators.minLength(5)]],
                    confirmPassword: ['',Validators.required],
                    firstName : ['',Validators.required],
                    lastName : ['',Validators.required],
                    role : [0,Validators.required],
                    branches : this.formBuilder.array(
                                    [{name : "Anikabury",code : "00001",id : "3fa85f64-5717-4562-b3fc-2c963f66afa6"}],
                                    Validators.required)
                  },{
                    validators : MustMatch('password','confirmPassword')
                  });
                  console.log(this.roles);
                  console.log(this.userEntryForm);
                  console.log(this.fc);
               }

  ngOnInit(): void {
    this.branches$ = this.branchService.get();
  }

  get fc() {
    return this.userEntryForm.controls;
  }

  newUser(){
      // const newUser = {data : this.userEntryForm};
      // this.userService.create(newUser).subscribe(
      //   res => {
      //     this.alertify.message('User added.');
      //     this.userStore.dispatch(UserManagementActions.fetchUsers());
      //     this.userEntryForm.reset();
      //     console.log(res);
      //   },
      //   error => {
      //     console.log(error);
      //     this.alertify.error(error);
      //   }
      // )
     
      console.log(this.userEntryForm);
      console.log(this.fc);
  }

  back(){
    this.location.back();
  }

  addRemoveRole(event : any, roleNum : number){

    if(event.currentTarget.checked){
      this.fc.role.setValue(this.fc.role.value + roleNum);
    }else{
      this.fc.role.setValue(this.fc.role.value - roleNum);
    }
  }

  addBranch(branch : any){
    this.fc.branches.patchValue(branch);
  }

}
