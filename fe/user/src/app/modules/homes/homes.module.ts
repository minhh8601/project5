import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IndexComponent } from './index/index.component';
import { DetailComponent } from './detail/detail.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ShopComponent } from './shop/shop.component';
import {NgxPaginationModule} from 'ngx-pagination';
@NgModule({
  declarations: [
    IndexComponent,
    DetailComponent,
    ShopComponent,

  ],
  imports: [
    CommonModule,NgbModule,FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    RouterModule.forChild([
      { path: 'detail/:id', component: DetailComponent },
      { path: 'index', component: IndexComponent },
      { path: 'shop', component: ShopComponent },

    ])
  ]
})
export class HomesModule { }
