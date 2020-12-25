import { Component, OnInit } from '@angular/core';

@Component({
  selector: "app-pie-chart",
  templateUrl: "./pie-chart.component.html",
  styleUrls: ["./pie-chart.component.css"],
})
export class PieChartComponent implements OnInit {
  single: any[];
  view: any[] = [0, 250];

  // options
  gradient: boolean = true;
  showLegend: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  legendPosition: string = "below";

  colorScheme = {
    domain: ["#5AA454", "#A10A28", "#C7B42C", "#AAAAAA"],
  };

  constructor() {
    Object.assign(this, { single });
  }

  onSelect(data): void {
    console.log("Item clicked", JSON.parse(JSON.stringify(data)));
  }

  onActivate(data): void {
    console.log("Activate", JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data): void {
    console.log("Deactivate", JSON.parse(JSON.stringify(data)));
  }

  ngOnInit(): void {}
}

export var single = [
  {
    name: "Aldeguer",
    value: 8940000,
  },
  {
    name: "Pototan",
    value: 5000000,
  },
  {
    name: "Guimaras",
    value: 7200000,
  },
  {
    name: "Roxas",
    value: 6200000,
  },
  {
    name: "Palawan",
    value: 6302300,
  },
];
