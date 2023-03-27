import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AuthenService } from '../../services/authen.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent  implements OnInit {
  currentImg!: string;
  currentLanguage!: string;
  showNavbar: boolean = true;
  showLogout:boolean = false;
  constructor(private translate: TranslateService,private authen : AuthenService){
    var lang = localStorage.getItem('lang');
    if(!lang) 
    {
      localStorage.setItem('lang', 'vn');
      translate.use('vn');
    }else translate.use(lang);
    let urlImg = localStorage.getItem('img');
    let langText = localStorage.getItem('langText');
    if(!urlImg)
    {
      localStorage.setItem('img','assets/imgs/covn.png');
      this.currentImg = 'assets/imgs/covn.png';
    }else this.currentImg = urlImg;
    if(!langText){
      localStorage.setItem('langText','Việt Nam');
      this.currentLanguage = 'Việt Nam';
    }else this.currentLanguage = langText;
    
  }
  ngOnInit():void{
    debugger
    if(this.authen.isLogin) {
      console.log('long',this.authen.isLogin)
      this.showLogout = true;
      this.showNavbar = false;
    }
  }
  selectedValue(lang:any)
  {
    if (lang === 'vn') {
      localStorage.setItem('img','assets/imgs/covn.png');
      localStorage.setItem('langText','Việt Nam');
    } else if (lang === 'en') {
      localStorage.setItem('img','assets/imgs/coanh.png');
      localStorage.setItem('langText','English');
    }
    this.translate.use(lang);
    localStorage.setItem('lang',lang);

  }
}
