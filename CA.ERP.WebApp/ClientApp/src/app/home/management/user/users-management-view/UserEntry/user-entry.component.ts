import { Component, ElementRef, OnInit, QueryList } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { UserManagementActions } from '../../action/user.action';
import { UserManagementState } from '../../reducers';
import { UserService } from '../../user.service';
import { Location } from '@angular/common';
import { MustMatch } from 'src/app/misc/validators/mustMatch';
import { Role } from '../../model/user.role';
import { BranchService } from '../../../branch/branch.service';
import { Observable } from 'rxjs';
import { BranchView } from '../../../branch/model/branch.view';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-entry',
  templateUrl: './user-entry.component.html',
  styleUrls: ['./user-entry.component.css']
})



export class UserEntryComponent implements OnInit {

  userEntryForm: FormGroup;
  branches$: Observable<BranchView[]>;
  selectedBranch: BranchView = undefined;
  submitted: boolean = false;
  roles = Object.values(Role).map(val => {
    return {
      name: val,
      num: Role[val],
      isSelected: false
    }
  }).splice(0, Object.keys(Role).length / 2);

  constructor(private formBuilder: FormBuilder,
    private userService: UserService,
    private userStore: Store<UserManagementState>,
    private activatedRoute: ActivatedRoute,
    private alertify: AlertifyService,
    private location: Location,
    private branchService: BranchService) {
    this.userEntryForm = this.formBuilder.group({
      id: [undefined],
      userName: ['', [Validators.required, Validators.minLength(5)]],
      password: ['', [Validators.required, Validators.minLength(5)]],
      confirmPassword: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      role: [null, Validators.required],
      branches: this.formBuilder.array(
        [],
        Validators.required)
    }, {
      validators: MustMatch('password', 'confirmPassword')
    });
    this.activatedRoute.params.subscribe(params => {
      if (params.id !== undefined) {
        activatedRoute.data.subscribe((data) => {
          if (data !== undefined) {
            this.userEntryForm.patchValue(data.data);
            this.populateUserBranch(data.data.userBranches);
            this.selectedRole(data.data.role)
            console.log(this.userEntryForm.value);
          }
        });
      }
    });

  }

  ngOnInit(): void {
    this.branches$ = this.branchService.get();
  }

  populateUserBranch(userBranches) {

    userBranches.forEach(branch => {
      this.branchesArray.push(new FormControl(branch));
    });

  }

  get fc() {
    return this.userEntryForm.controls;
  }

  get branchesArray(): FormArray {
    return this.fc.branches as FormArray;
  }

  get selectedBranchGroup(): FormGroup {
    return this.formBuilder.group({
      branchId: this.selectedBranch.id,
      name: this.selectedBranch.name,
      code: this.selectedBranch.code
    });
  }



  removeBranch(index: number) {
    this.branchesArray.removeAt(index);
    this.alertify.warning('Selected branch removed.');
  }

  newUser() {

    this.submitted = true;
    if (!this.userEntryForm.invalid) {
      const newUser = { data: this.userEntryForm.value };
      this.userService.create(newUser).subscribe(
        res => {
          this.alertify.message('User added.');
          this.userStore.dispatch(UserManagementActions.fetchUsers());
          this.userEntryForm.reset();
          this.branchesArray.clear();
          this.selectedBranch = undefined;
          this.uncheckRoleCheckbox();
        },
        error => {
          this.alertify.error(`Error ${error.status} : ${error.title}`);
        }
      )
    } else {
      this.alertify.error('Please fill up all required fields.')
    }
  }

  updateUser() {
    this.submitted = true;
    const updateUser = { data: this.userEntryForm.value };
  
    this.userService.update(this.fc.id.value, updateUser).subscribe(
      res => {
        this.alertify.message('User updated.');
        this.userStore.dispatch(UserManagementActions.fetchUsers());
      },
      error => {
       console.log(error);
        this.alertify.error(`Error ${error.status} : ${error.title}`);

      }
    )
  }

  selectedRole(rolesFlag: number) {
    for (var enumFlag in Role) {
      var val = Number(Role[enumFlag]);
      if ((rolesFlag & val) === val) {
        this.roles.forEach(role => {
          if (role.num === val) {
            role.isSelected = true;
          }
        });
      }
    }
  }


  back() {
    this.location.back();
  }

  addRemoveRole(event: any, roleNum: number, index: number) {
    if (event.currentTarget.checked) {
      this.fc.role.setValue(this.fc.role.value + roleNum);
      this.roles[index].isSelected = true;
    } else {
      this.fc.role.setValue(this.fc.role.value - roleNum);
      this.roles[index].isSelected = false;
    }

  }


  uncheckRoleCheckbox() {
    this.roles.forEach(element => {
      element.isSelected = false;
    });
    console.log(this.roles);
  }

  addBranch() {
    if (!this.selectedBranch) {
      this.alertify.error('Please select branch.');
      return;
    }
    for (let index = 0; index < this.branchesArray.length; index++) {
      const element = this.branchesArray.controls[index];
      if (element.value.branchId === this.selectedBranchGroup.controls.branchId.value) {
        this.alertify.error('Branch exists');
        return;
      }
    }
    this.branchesArray.push(this.selectedBranchGroup);
  }




}
