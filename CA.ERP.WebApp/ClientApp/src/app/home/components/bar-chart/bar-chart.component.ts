import { Component, Input, OnInit } from '@angular/core';
import { single } from '../pie-chart/pie-chart.component';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css'],
})
export class BarChartComponent implements OnInit {
  single: any[];
  multi: any[];
  @Input() reportName: string;
  @Input() yLabel: string;
  @Input() xLabel: string;
  @Input() data: any[];
  view: any[] = [0, 0];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = true;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = '';
  showYAxisLabel = true;
  yAxisLabel = '';

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA'],
  };

  constructor() {
    Object.assign(this, { single });
  }

  onSelect(event) {
    console.log(event);
  }
  ngOnInit(): void {
       this.yAxisLabel = this.yLabel;
       this.xAxisLabel = this.xLabel;
  }
}
