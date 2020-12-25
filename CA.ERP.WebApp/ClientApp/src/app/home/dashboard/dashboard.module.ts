import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardViewComponent } from './dashboard-view/dashboard-view.component';
import { AnouncementsComponent } from '../components/anouncements/anouncements.component';
import { BarChartComponent } from '../components/bar-chart/bar-chart.component';
import { PieChartComponent } from '../components/pie-chart/pie-chart.component';
import { DashboardCalendarComponent } from '../components/dashboard-calendar/dashboard-calendar.component';
import { NgxBootstrapModulesModule } from 'src/app/ngx-bootstrap-modules/ngx-bootstrap-modules.module';


@NgModule({
  declarations: [
    PieChartComponent,
    AnouncementsComponent,
    BarChartComponent,
    DashboardCalendarComponent,
    DashboardViewComponent,
  ],
  imports: [
    CommonModule,
    NgxChartsModule,
    DashboardRoutingModule,
    NgxBootstrapModulesModule,
  ],
})
export class DashboardModule {}
