import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard-view',
  templateUrl: './dashboard-view.component.html',
  styleUrls: ['./dashboard-view.component.css']
})
export class DashboardViewComponent implements OnInit {
 single: any[] = [
  {
    name: 'Aldeguer',
    value: 8940000,
  },
  {
    name: 'Pototan',
    value: 5000000,
  },
  {
    name: 'Guimaras',
    value: 7200000,
  },
  {
    name: 'Roxas',
    value: 6200000,
  },
  {
    name: 'Palawan',
    value: 6302300,
  },
];

  constructor() { }

  ngOnInit() {
  }

}
