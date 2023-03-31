import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/services/user-service.service';
import { UserModel } from '../user.model';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.scss']
})
export class UserEditorComponent implements OnInit {
  @Output() refreshList = new EventEmitter<void>();
  @Input() user: any;
  birthdayFormat!: string;
  passShow!: boolean;
  rePassword!: string;
  checksubmit: boolean = false;
  emailRegex: boolean = true;
  incorrectPassword: boolean = false;
  isEnoughAge: boolean = false;
  birthday!: string;
  constructor(private userService: UserService, private toastr: ToastrService) {

  }
  ngOnInit(): void {
    var date = new Date(this.user.birthday);
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();
    this.birthday = `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
  }

  togglePasswordShow() {
    this.passShow = !this.passShow;
  }

  onSubmit(form: any) {
    this.checksubmit = true;
    if (this.rePassword !== this.user.password) {
      this.incorrectPassword = true;
    } else this.incorrectPassword = false;
    this.isEnoughAge = checkAge(this.birthday);
    this.emailRegex = isValidEmail(this.user.email);
    if (form.invalid) {
      return;
    }
    let newUser = new UserModel();
    newUser.name = this.user.name;
    newUser.loginName = this.user.loginName;
    newUser.password = this.user.password;
    newUser.gender = this.user.gender;
    newUser.birthday = this.birthday;
    newUser.phone = this.user.phone;
    newUser.email = this.user.email;
    newUser.isAdmin = this.user.isAdmin;
    if (this.user.id === 0) {
      this.userService.addUser(newUser).subscribe((data:any) => {

        if (data.message === 'Đã tồn tại user') {
          this.toastr.error('Tên đăng nhập đã tồn tại', 'Add user fail');
        } else {

          this.refreshList.emit();
          this.toastr.success('Thêm nhân viên thành công', 'Add user success')
          form.resetForm();
        }
      }, (err) => {

        this.toastr.error('Thêm nhân viên thất bại', 'Add user fail');
        form.resetForm();
      })
    } else {

      newUser.id = this.user.id;
      this.userService.updateUser(newUser).subscribe((data:any) => {

        console.log(data);
        if(data.success){
          this.refreshList.emit();
          this.toastr.success('Cập nhật nhân viên thành công', 'Update success', {
            closeButton: true
          });
          form.resetForm();
        }else {
          this.toastr.error('Cập nhật nhân viên thất bại', 'Update user fail', {
            closeButton: true
          });
          form.resetForm();
        }
       
      }, (err) => {
        this.toastr.error('Cập nhật nhân viên thất bại', 'Update user fail', {
          closeButton: true
        });
        form.resetForm();
      })
    }
  }
}

function isValidEmail(email: string): boolean {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;
  return emailRegex.test(email);
}

function checkAge(birthdate: string): boolean {
  var birthday = new Date(birthdate);
  const today = new Date();
  let age = today.getFullYear() - birthday.getFullYear();
  if (age >= 18) {
    return true;
  } else return false;
}