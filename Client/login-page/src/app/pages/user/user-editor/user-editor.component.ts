import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.scss']
})
export class UserEditorComponent implements OnInit {

  @Input() user:any;
  birthdayFormat!:string;

  constructor(){

  }
  ngOnInit(): void {
    
  }
  editUser(){

  } 
  addUser(){
    
  }
}
