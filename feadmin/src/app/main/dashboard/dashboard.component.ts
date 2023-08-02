import { AfterViewInit, Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base-component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent extends BaseComponent implements OnInit, AfterViewInit {

  constructor(injector: Injector) {
    super(injector)
   }

  ngOnInit(): void {
  }
  ngAfterViewInit() {
    this.loadScripts('./assets/plugins/common/common.min.js','./assets/js/custom.min.js',
    './assets/js/settings.js','./assets/js/gleek.js','./assets/js/styleSwitcher.js',
    './assets//plugins/chart.js/Chart.bundle.min.js','./assets/plugins/circle-progress/circle-progress.min.js','./assets/plugins/d3v3/index.js',
    './assets/plugins/topojson/topojson.min.js','./assets/plugins/datamaps/datamaps.world.min.js','./assets/plugins/raphael/raphael.min.js',
    './assets/plugins/morris/morris.min.js','./assets/plugins/moment/moment.min.js',
    './assets/plugins/pg-calendar/js/pignose.calendar.min.js','./assets/plugins/chartist/js/chartist.min.js',
    './assets/plugins/chartist-plugin-tooltips/js/chartist-plugin-tooltip.min.js','./assets/js/dashboard/dashboard-1.js');
  }

}
