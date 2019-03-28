import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service'
import { FormGroup, FormControl, FormBuilder, Validators, FormArray } from '@angular/forms';
import { User } from '../../models/user';
import { Logger } from '../../services/logger.service';
import { UserInfoService } from '../../services/user-info.service';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { UserUpdate } from '../../models/user-update';
import { userInfo } from 'os';

@Component({
  selector: 'app-cabinet-edit-profile',
  templateUrl: './user-edit.component.html'
})
export class UserEditComponent implements OnInit {

  userName: any;
  user: User;
  userUpdate: UserUpdate;
  isAdmin: boolean;
  userId: string;

  userForm: FormGroup
  constructor(private fb: FormBuilder,
    private userService: UserService,
    private authService: AuthService,
    private actRoute: ActivatedRoute,
    private userInfoService: UserInfoService
  ) { }

  ngOnInit() {
    this.userId = this.actRoute.snapshot.params['id'];
    this.isAdmin = this.userInfoService.roles.includes('Admin');
    this.userService.getUserById(this.userId).subscribe((res: User) => {
      this.user = res;
      this.initializeForm();
    }, error => this.handleError(error));
    
  }

  initializeForm() {
    this.userForm = new FormGroup({
      userName: new FormControl(this.user.userName),
      email: new FormControl(this.user.email),
    });
    
    Logger.log('Form initialized.');
    Logger.log(this.userForm);
  }

  onSubmit() {
    

    let formData = this.userForm.value;

    this.userUpdate = formData;
    this.userUpdate.isBlocked = this.user.isBlocked;
    this.userUpdate.approvalStatus = this.user.approvalStatus;

    Logger.log(this.userUpdate);
    this.userService.updateUser(this.userUpdate, this.userId)
        .subscribe(result => {
          Logger.log(result);
          console.log(result);
          this.authService.refresh();
        }, error => this.handleError(error));

 
  }

  blockUser() {
    this.userService.blockUser(this.userId, true).subscribe(() => {
      this.user.isBlocked = true;
    }, error => this.handleError(error));
  }

  unBlockUser() {
    this.userService.blockUser(this.userId, false).subscribe(() => {
      this.user.isBlocked = false;
    }, error => this.handleError(error));
  }

  approveUser() {
    this.userService.approvalUser(this.userId, true).subscribe(() => {
      this.user.approvalStatus = true;
    }, error => this.handleError(error));
  }

  rejectUser() {
    this.userService.approvalUser(this.userId, false).subscribe(() => {
      this.user.approvalStatus = false;
    }, error => this.handleError(error));
  }

  handleError(error: any) {
    console.log(error);
    //this.apiError = error['status'];

    //if (error['error'] != undefined)
    //  this.apiError += ': ' + error['error']['Message'];
  }
}