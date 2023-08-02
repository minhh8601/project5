import { OrderComponent } from './order/order.component';
import { DanhmucComponent } from './danhmuc/danhmuc.component';
import { NhasanxuatComponent } from './nhasanxuat/nhasanxuat.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { MainRoutes } from './main.route';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SanphamComponent } from './sanpham/sanpham.component';
import {NgxPaginationModule} from 'ngx-pagination';
import { HrmComponent } from './hrm/hrm.component';





@NgModule({
  declarations: [
    MainComponent, DashboardComponent,
     SanphamComponent, NhasanxuatComponent,
    DanhmucComponent, OrderComponent, HrmComponent],
  imports: [

    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forChild(MainRoutes),
    NgxPaginationModule,


  ]
})
export class MainModule { }
