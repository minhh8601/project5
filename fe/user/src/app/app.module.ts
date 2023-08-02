import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { PreloadAllModules, RouterModule } from '@angular/router';
import { Approutes } from './app.route';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import{HttpClientModule} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {NgxPaginationModule} from 'ngx-pagination';
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    SharedModule,
    HttpClientModule,
    RouterModule.forRoot(Approutes,{preloadingStrategy: PreloadAllModules}),
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
