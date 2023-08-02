import { Component, OnInit, AfterViewInit, Injector, inject } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base-component';
import { CartService } from 'src/app/core/services/cart.service';
import { SendService } from 'src/app/core/services/send.service';


@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent extends BaseComponent implements OnInit, AfterViewInit {

  public item: any;
  public itemhot:any;
  public itemsame:any;
  public idsame:any;
  constructor(injector: Injector, private cart: CartService, private send: SendService) {
    super(injector);
  }

  public AddToCart(item: any) {
    this.cart.addToCart(item);
    this.send.addObjct(this.cart.getItems().length);
    alert('Thêm thành công sản phẩm vào giỏ hàng!');
  }

  ngOnInit() {
    this.GetHot();
    this._route.params.subscribe(params => {
      let id = params['id'];
      this._api.get('/api/sanpham/get-by-id/'+id).subscribe(res => {
        this.item = res;
        this.idsame=res.idDanhMuc

        this._api.get('/api/sanpham/get-sp-cung-loai/'+this.idsame).subscribe(res => {
          this.itemsame = res;
        });
      });
    });

  }

  public GetHot(){
    this._api.get('/api/sanpham/get-hot').subscribe(res => {
      this.itemhot = res;
    });

  }

  ngAfterViewInit() {

      this.loadScripts('./assets/js/jquery-3.3.1.min.js','./assets/js/bootstrap.min.js',
    './assets/js/jquery.nice-select.min.js','./assets/js/jquery-ui.min.js',
    './assets/js/jquery.slicknav.js','./assets/js/mixitup.min.js','./assets/js/owl.carousel.min.js','./assets/js/main.js');

  }
}
