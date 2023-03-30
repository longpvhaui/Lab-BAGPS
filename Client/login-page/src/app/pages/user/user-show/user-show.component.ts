import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthenService } from 'src/app/shared/services/authen.service';
import { UserService } from 'src/app/shared/services/user-service.service';
import { UserEditorComponent } from '../user-editor/user-editor.component';
import { UserModel } from '../user.model';
import { UserSearch } from './user-search.model';

@Component({
  selector: 'app-user-show',
  templateUrl: './user-show.component.html',
  styleUrls: ['./user-show.component.scss']
})
export class UserShowComponent implements OnInit {
  constructor(private userService : UserService, private toastr : ToastrService, private authen:AuthenService){

  }
  showModal: boolean = false;
  userList:any = [];
  listId:any =[];
  isAdmin!:boolean;
  title!:string;
  activeAddEditComp!: boolean;
  user!:UserModel;
  searchText!:string;
  dateFrom!: string;
  dateTo!:string;
  genderSelected!:number;
  page = 1;
  pageSize = 10;
  pageSizeOptions = [5, 10, 20];
  totalItems = this.userList.length;
  ngOnInit(){

    this.isAdmin = this.authen.isAdmin;
    this.refreshList();

  }
  clearFilter(form:any){

    form.resetForm();
    this.refreshList();
  }
  onSubmit(form:any){
    let userSearch = new UserSearch();

      userSearch.searchText = form.value.searchText;
      userSearch.fromDate = form.value.fromDate;
      userSearch.toDate = form.value.toDate;
      userSearch.gender = form.value.gender;
      this.userService.getPagging(userSearch).subscribe((data)=>{
        this.userList = data;
      })
  }
  refreshList(){

    this.userService.getAllUser().subscribe((data:any)=>{
      this.userList = data;
    })
    this.activeAddEditComp = false;
    this.showModal = false;
  }
  closeClick(){
    this.refreshList();
  }
  deleteUser(data:any){

    if(confirm('Are you sure?')){
      this.userService.deleteUser(data.id).subscribe((data)=>{
        this.toastr.success('Xóa nhân viên thành công', 'Delete success',{
          closeButton :true
        })
        this.refreshList();
      },(error)=>{
          this.toastr.error('Xóa nhân viên thất bại', 'Delete fail',{
          closeButton :true
        })
      });
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
    this.showModal = true;
    this.title = 'Add user';
    this.activeAddEditComp = true;
  }
  editClick(data:any){
    this.showModal = true;
    this.user = data;
    this.activeAddEditComp = true;
    this.title = 'Update user';
  }

  handleChange(e:any){

    const index = this.listId.indexOf(e.target.value);
    if (e.target.checked) {
    this.listId.push(e.target.value);
    } else {
    this.listId.splice(index, 1); // Remove 1 item at index
    }


  }
  deleteMultiUser(){
    this.userService.deleteMultiUser(this.listId).subscribe((res)=>{
        this.toastr.success('Xóa nhân viên thành công', 'Delete success',{
          closeButton :true
        });
        this.refreshList();
    },
    (err)=>{
      this.toastr.success('Xóa nhân viên thành công', 'Delete success',{
        closeButton :true
      });
      this.refreshList();
    })
  }
}
