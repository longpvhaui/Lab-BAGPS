import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { interval } from 'rxjs';
import { AuthenService } from '../services/authen.service';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent implements OnInit {
  title!: string;
  content!: string;
  currentIndex: number = 1;
  passShow!: boolean;
  defaultBanner: Slide = {

    id: 0,
    title: 'Giải pháp điều hành vận tải',
    content: 'Camera giám sát hành trình trong xe ô tô của BA GPS mang đến nhiều lợi ích cho doanh nghiệp',
    imagePath: 'assets/imgs/ba1.jpg',
    IsActived: 1,
    IsDeleted: 0,
    TypeOfNews: 1,

  }
  username!: string;
  password!: string;
  myForm!: FormGroup;

  loginForm!: FormGroup;
  slides: Slide[] = [
    {
      id: 1,
      title: 'Giải pháp điều hành vận tải',
      content: 'Camera giám sát hành trình trong xe ô tô của BA GPS mang đến nhiều lợi ích cho doanh nghiệp',
      imagePath: 'assets/imgs/ba1.jpg',
      IsActived: 1,
      IsDeleted: 0,
      TypeOfNews: 1,
    },
    {
      id: 2,
      title: 'Giải pháp tổng đài',
      content: 'Chăm sóc khách hàng tận tình',
      imagePath: 'assets/imgs/ba2.jpg',
      IsActived: 1,
      IsDeleted: 0,
      TypeOfNews: 1,
    },
    {
      id: 3,
      title: 'Giải pháp điều hành vận tải 2',
      content: 'Camera giám sát hành trình trong xe ô tô của BA GPS mang đến nhiều lợi ích cho doanh nghiệp',
      imagePath: 'assets/imgs/err',
      IsActived: 0,
      IsDeleted: 0,
      TypeOfNews: 1,
    },
    {
      id: 4,
      title: 'Giải pháp điều hành vận tải 3',
      content: 'Camera giám sát hành trình trong xe ô tô của BA GPS mang đến nhiều lợi ích cho doanh nghiệp',
      imagePath: 'assets/imgs/rrr',
      IsActived: 0,
      IsDeleted: 0,
      TypeOfNews: 1,
    },
  ]
  currentSlide: Slide = new Slide();


  users: UserLogin[] = [
    {
      userName: 'admin',
      passWord: 'admin'
    },
    {
      userName: '123',
      passWord: '123'
    }
  ]
  constructor(private route: Router,private translate: TranslateService,private authen : AuthenService,private toastr: ToastrService) {
    var lang = localStorage.getItem('lang');
    if(!lang) 
    {
      localStorage.setItem('lang', 'vn');
      translate.use('vn');
    }else translate.use(lang);
    
    interval(5000).subscribe(() => {
      let slideShow = this.slides.filter(x => x.IsActived === 1 && x.IsDeleted === 0 && x.TypeOfNews === 1)
      let currentSlide = slideShow.find(slide => slide.id === this.currentIndex);
      if (currentSlide) {
        this.currentIndex = currentSlide.id % slideShow.length + 1;
        this.content = this.slides[this.currentIndex - 1].content;
        this.title = this.slides[this.currentIndex - 1].title;
      }
    });
  }
  ngOnInit(): void {
    if (this.slides) {
      this.content = this.slides[0].content;
      this.title = this.slides[0].title;

    }
  }
   

  getCurrentSlideUrl() {
    return `url('${this.slides[this.currentIndex - 1].imagePath}')`;
  }
  handleDot(e: any) {
    this.currentIndex = e.id;
    this.title = e.title;
    this.content = e.content;
  }

  togglePasswordShow() {
    this.passShow = !this.passShow;
  }
  async login(e: any) {

    if (this.username === '' || this.password === '') {
      alert('Không được bỏ trống tài khoản hoặc mật khẩu')
      return
    }
    else {
      const result = await this.authen.logIn(this.username, this.password);
      if(result) {
           this.route.navigate(['user']);
           this.toastr.success(`Hello, ${this.username}!`, 'Login success',{
            closeButton :true
          })
        }
        else {
          this.toastr.error('Đăng nhập thất bại', 'Login fail',{
            closeButton :true
          })
        }
    }
  }

}
// var user = this.users.find(x=>x.userName === this.username && x.passWord === this.password)
    // if (user) {
    //   this.route.navigate(['user'])
    // }
    // else alert('Tài khoản hoặc mật khẩu chưa chính xác')

export class Slide {
  id!: number;
  title!: string;
  content!: string;
  imagePath!: string;
  IsActived!: number;
  IsDeleted!: number;
  TypeOfNews!: number;
}

export class UserLogin {
  userName!: string;
  passWord!: string;
}