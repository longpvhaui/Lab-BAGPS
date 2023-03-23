import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/services/user-service.service';
import { UserModel } from '../user.model';

@Component({
  selector: 'app-user-show',
  templateUrl: './user-show.component.html',
  styleUrls: ['./user-show.component.scss']
})
export class UserShowComponent implements OnInit {
  constructor(private userService : UserService){

  }
  userList:any = [
  ];
  title!:string;
  activeAddEditComp!: boolean;
  user!:UserModel;
  ngOnInit():void{
    this.refreshList();
  }
  refreshList(){
    this.userService.getAllUser().subscribe((data:any)=>{
      this.userList = data;
      console.log(data)
    })
  }
  closeClick(){
    this.activeAddEditComp = false;
    this.refreshList();
  }
  deleteUser(data:any){
    if(confirm('Are you sure?')){
      //call api
    }
  }
  addClick(){
    this.user = {
      id : 0 ,
      gender : 0,
      name : '',
      loginName : '',
      password:'',
      birthday: '',
      phone : '',
      email : '',
      isAdmin: false
    }
    this.title = 'Add user';
    this.activeAddEditComp = true;
  }
  editClick(data:any){
    this.user = data;
    this.activeAddEditComp = true;
    this.title = 'Update user';
  }
}
