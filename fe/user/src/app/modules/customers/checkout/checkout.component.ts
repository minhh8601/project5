import { Component, OnInit, inject, Injector, AfterViewInit } from '@angular/core';
import { FormControl, FormGroup, Validators,} from '@angular/forms';
import { BaseComponent } from 'src/app/core/common/base-component';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent extends BaseComponent implements OnInit,AfterViewInit {
  public formCus!: FormGroup;
  public items: any;
  public TongTien: any;
  constructor(injector: Injector) {
    super(injector);
  }

   ngOnInit(): void {
    this.items = JSON.parse(localStorage.getItem('cart') || '[]');
    this.TongTien = this.items.reduce((sum: any, x: any) => sum + x.gia * x.quantity, 0);
    this.formCus = new FormGroup({
      'txt_hoten': new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]),
      'txt_diachi': new FormControl('', [Validators.required]),
      'txt_sodienthoai': new FormControl('', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
      'txt_email': new FormControl('', [Validators.required,Validators.email]),
      'txt_ghichu': new FormControl('')
    });

  }
  get hoten() {
    return this.formCus.get('txt_hoten')!;
  }
  get diachi() {
    return this.formCus.get('txt_diachi')!;
  }
  get sodienthoai() {
    return this.formCus.get('txt_sodienthoai')!;
  }
  get email() {
    return this.formCus.get('txt_email')!;
  }
  get ghichu(){
    return this.formCus.get('txt_ghichu');
  }

  public onSubmit(val: any) {
    if (this.formCus.invalid) {
      return;
    }
    debugger;
    let obj: any = {};
    obj.khach = {
      TenKhachHang: val.txt_hoten,
      DiaChi: val.txt_diachi,
      SoDienThoai: val.txt_sodienthoai,
      Email: val.txt_email,
      GhiChu: val.txt_ghichu
    };
    obj.donhang = [];
    this.items.forEach((x: any) => {
      obj.donhang.push({
        IdSanPham: x.idSanPham,
        SoLuong: x.quantity,
        GiaMua: x.gia
      });
    });
    console.log(obj);
    debugger;
    this._api.post('/api/cart/create-cart', obj).subscribe(res => {
      if (res && res.data) {
        alert('Đặt hàng thành công')
      } else {
        alert('Error')
      }
    });
  }


  ngAfterViewInit() {
    setTimeout(() => {
      this.loadScripts('./assets/js/jquery-3.3.1.min.js','./assets/js/bootstrap.min.js',
      './assets/js/jquery.nice-select.min.js','./assets/js/jquery-ui.min.js',
      './assets/js/jquery.slicknav.js','./assets/js/mixitup.min.js','./assets/js/owl.carousel.min.js','./assets/js/main.js');
    });
   }

}
