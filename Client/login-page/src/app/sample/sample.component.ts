import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sample',
  templateUrl: './sample.component.html',
  styleUrls: ['./sample.component.scss']
})
export class SampleComponent implements OnInit {
  text :string = "long";
  arrData =   [1,2,3,4,5,6,7,8,9,10,11,12]
  
  ngOnInit(): void {


    }


  }
