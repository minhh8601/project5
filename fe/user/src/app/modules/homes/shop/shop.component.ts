import { Component, OnInit, AfterViewInit, Injector, inject } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base-component';
@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent extends BaseComponent implements OnInit {
  public itemhot:any;
  public items:any;
  public loc:any;
  public page: any = 1;
  public id: any;
  public pageSize: any = 12;
  public totalItem: any;
  p:number=1;
  sortProperty: string = 'id';
  sortOrder = 1;


  constructor( injector: Injector) {
    super(injector);
   }

  ngOnInit() {
    this.GetHot();

    this.loc = localStorage.getItem('loc') || '';
    this._route.params.subscribe(params => {

      this.id = params['id'];
      this._api.post('/api/danhmucsanpham/search', { loc: this.loc, page: this.page, pageSize: this.pageSize, id_danh_muc: this.id }).subscribe(res => {
        this.items = res.data;
        this.totalItem = res.totalItem;


      });

    });


  }
  public GetHot(){
    this._api.get('/api/sanpham/get-hot').subscribe(res => {
      this.itemhot = res;
    });

  }

  public loadPage(page: any) {
    this._api.post('/api/danhmucsanpham/search', {loc: this.loc,  page: page, pageSize: this.pageSize, id_danh_muc: this.id }).subscribe(res => {
      this.items = res.data;
      this.totalItem = res.totalItem;

    });
  }

  public loadData(pageSize:any) {
    this.pageSize = pageSize;
     this._api.post('/api/danhmucsanpham/search', { loc: this.loc, page: 1, pageSize: pageSize, id_danh_muc: this.id }).subscribe(res => {
       this.items = res.data;
       this.totalItem = res.totalItem;

     });
   }

   setDieuKienLoc(loc: any) {
    this.loc = loc;
    localStorage.setItem('loc',loc);
    this.loadData(this.pageSize);
  }
  sortBy(property: string) {
    this.sortOrder = property === this.sortProperty ? (this.sortOrder * -1) : 1;
    this.sortProperty = property;
    this.items = [...this.items.sort((a: any, b: any) => {
        // sort comparison function
        let result = 0;
        if (a[property] < b[property]) {
            result = -1;
        }
        if (a[property] > b[property]) {
            result = 1;
        }
        return result * this.sortOrder;
    })];
}

sortIcon(property: string) {
    if (property === this.sortProperty) {
        return this.sortOrder === 1 ? '☝️' : '👇';
    }
    return '';
}


  ngAfterViewInit() {
    this.loadScripts('assets/js/jquery-3.3.1.min.js','assets/js/bootstrap.min.js',
      'assets/js/jquery.nice-select.min.js','assets/js/jquery-ui.min.js',
      'assets/js/jquery.slicknav.js','assets/js/mixitup.min.js','assets/js/owl.carousel.min.js','assets/js/main.js');


 }
}
