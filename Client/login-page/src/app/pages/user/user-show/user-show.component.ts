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
  currentPage!:number;
  page:number = 1
  pageSize:number = 10;
  pageSizeOptions = [5, 10, 20];
  totalItems:number =0;
  startIndex!:number;
  userSearch!:UserSearch;
  isSearch:boolean = false;
  ngOnInit(){

    this.isAdmin = this.authen.isAdmin;
    this.refreshList();
    this.startIndex = (this.page - 1) * this.pageSize;

  }
  clearFilter(form:any){
    this.isSearch = false;
    form.resetForm();
    this.refreshList();
    
  }
  onSubmit(form:any){

    this.isSearch = true;
    this.userSearch = new UserSearch();
      this.userSearch.searchText = form.value.searchText;
      this.userSearch.fromDate = form.value.fromDate;
      this.userSearch.toDate = form.value.toDate;
      this.userSearch.gender = form.value.gender;
      this.userSearch.pageIndex = this.page;
      this.userSearch.pageSize = this.pageSize;
      this.userService.getPagging(this.userSearch).subscribe((data:any)=>{
        this.userList = data.users;
      })
  }
  refreshList(){
  
    if(this.isSearch){
      this.userService.getPagging(this.userSearch).subscribe((data:any)=>{
        this.userList = data.users;
      })
      this.activeAddEditComp = false;
      this.showModal = false;
    }else{
    this.userService.getUser(this.page,this.pageSize).subscribe((data:any)=>{
      this.userList = data.users;
      console.log(data);
       this.totalItems = data.totalItems;
       console.log(this.totalItems);
    })
    this.activeAddEditComp = false;
    this.showModal = false;}
  }
  closeClick(){
    this.refreshList();
  }
  changePage(e:any){

    this.page = e;
    this.currentPage = e;
    this.startIndex = (this.currentPage - 1) * this.pageSize;
    this.refreshList();
  }
  onPageSizeChange(value:any){

    this.pageSize = value;
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
    if(confirm('Are you sure?')){
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
  }}
}
