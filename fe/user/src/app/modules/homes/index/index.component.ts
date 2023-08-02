import { AfterViewInit, Component, Injector, OnInit, Renderer2 } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base-component';


@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})

export class IndexComponent extends BaseComponent implements OnInit, AfterViewInit {
  p:number=1;
  public items:any;
  public itemhot:any;
  constructor( injector: Injector) {
    super(injector);
   }

  ngOnInit(): void {
    this.GetHot();

    this._api.get('/api/sanpham/get-all').subscribe(res => {
      this.items = res;

      // setTimeout(() => {
      //   this.loadScripts('./assets/js/jquery-3.3.1.min.js','./assets/js/bootstrap.min.js',
      //   './assets/js/jquery.nice-select.min.js','./assets/js/jquery-ui.min.js',
      //   './assets/js/jquery.slicknav.js','./assets/js/mixitup.min.js','./assets/js/owl.carousel.min.js','./assets/js/main.js');
      // });
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
      };

}
