import { HrmComponent } from './hrm/hrm.component';
import { OrderComponent } from './order/order.component';
import { DanhmucComponent } from './danhmuc/danhmuc.component';

import { SanphamComponent } from './sanpham/sanpham.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Component } from '@angular/core';

import { MainComponent } from './main.component';
import { RouterModule, Routes } from '@angular/router';
import { NhasanxuatComponent } from './nhasanxuat/nhasanxuat.component';

export const MainRoutes: Routes = [
  {
    path: '', component: MainComponent,
    children: [
      {path:'',component:DashboardComponent},
      {path:'sanpham',component:SanphamComponent},
      {path:'nhasanxuat',component:NhasanxuatComponent},
      {path:'danhmuc',component:DanhmucComponent},
      {path:'order',component:OrderComponent},
      {path:'hrm',component:HrmComponent},


    ]
  }
];

