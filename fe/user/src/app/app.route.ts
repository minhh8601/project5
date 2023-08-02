import {  Routes } from '@angular/router';



export const Approutes: Routes = [
  { path: '', loadChildren: () => import('./modules/modules.module').then(m => m.ModulesModule)},
 
];
