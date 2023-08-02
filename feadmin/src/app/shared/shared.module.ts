import { FooterComponent } from './layout/footer/footer.component';
import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { NavbarComponent } from './layout/navbar/navbar.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    NavbarComponent,
    SidebarComponent,
    FooterComponent,
    
  ],
  exports: [
    NavbarComponent,
    SidebarComponent,
    FooterComponent,


  ],
  imports: [
    CommonModule,
    RouterModule
  ]
})
export class SharedModule { }
