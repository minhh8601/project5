import { Component, OnInit, AfterViewInit, Injector, inject } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/core/common/base-component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent extends BaseComponent implements OnInit, AfterViewInit {

  public list: any;
  public TongTien: any;
  constructor(injector: Injector, private router: Router,) {
    super(injector);
  }

  public Checkout () {
    this.router.navigate(['/customers/checkout']);
  }

  ngOnInit() {
      this.list = JSON.parse(localStorage.getItem('cart') || '[]');
      this.TongTien = this.list.reduce((sum:any, x:any) => sum +  x.gia * x.quantity, 0);
  }
  ngAfterViewInit() {
    this.loadScripts('../assets/js/jquery-3.3.1.min.js','../assets/js/bootstrap.min.js',
          '../assets/js/jquery.nice-select.min.js','../assets/js/jquery-ui.min.js',
          '../assets/js/jquery.slicknav.js','../assets/js/mixitup.min.js',
          '../assets/js/owl.carousel.min.js','../assets/js/main.js','' );

   }

   public add(idSanPham: any) {
    var index = this.list.findIndex((x: any) => x.idSanPham == idSanPham);
    if (index >= 0) {
      this.list[index].quantity += 1;
      this.TongTien = this.list.reduce((sum:any, x:any) => sum + x.gia  * x.quantity, 0);
    }
  }

  public reduce(idSanPham: any) {
    var index = this.list.findIndex((x: any) => x.idSanPham == idSanPham);
    if (index >= 0 && this.list[index].quantity >= 1) {
      this.list[index].quantity -= 1;
      this.TongTien = this.list.reduce((sum:any, x:any) => sum + x.gia  * x.quantity, 0);
    }
  }

  public deleteOne(idSanPham: any) {
    if (confirm("Bạn muốn xóa sản phẩm này khỏi giỏ hàng?")) {
      var index = this.list.findIndex((x: any) => x.idSanPham == idSanPham);
      if (index >= 0) {
        this.list.splice(index, 1);
        this.TongTien = this.list.reduce((sum:any, x:any) => sum + x.gia  * x.quantity, 0);
      }
    }
  }

  public deleteAll() {
    if (confirm("Bạn muốn xóa tất cả sản phẩm khỏi giỏ hàng?")) {
        localStorage.setItem('cart','');
        this.list = null;
        this.TongTien = 0;
    }
  }

  public updateCart() {
    localStorage.setItem('cart', JSON.stringify(this.list));
    alert("Cập nhật thông tin giỏ hàng thành công!");
  }

}
