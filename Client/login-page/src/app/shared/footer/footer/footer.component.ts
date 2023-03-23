import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {
   brands :Brand[] = [
    { 
      brandName : 'Trụ sở Hà Nội',
      description : 'Lô 14 Nguyễn Cảnh Dị, P Đại Kim, Q Hoàng Mai, TP Hà Nội'
    },
    { 
      brandName : 'Hải Phòng',
      description : 'Căn BH 01- 47 Khu đô thị Vinhomes Imperia, Đ Bạch Đằng , P Thượng Lý, Q Hồng Bàng, TP Hải Phòng'
    },
    { 
      brandName : 'Chi nhánh Miền Trung',
      description : 'Số B5-15, ngõ 26, Đ Nguyễn Thái Học, TP Vinh, Nghệ An'
    },
    { 
      brandName : 'Đà Nẵng',
      description : 'Lô 1 Khu B2-19 , KĐT Biệt thự sinh thái , Công viên Văn Hóa Làng quê và quần thể du lịch sông nước, P Hòa Qúy, Ngũ Hành Sơn, TP Đà Nẵng'
    },
    { 
      brandName : 'TP. Hồ Chí Minh',
      description : 'Số 9, Đường 3, KĐT Vạn Phúc, P Hiệp Bình Phước , TP Thủ Đức , TP Hồ Chí Minh'
    }
  ]
}

export class Brand {
  brandName!: string;
  description!: string;
}