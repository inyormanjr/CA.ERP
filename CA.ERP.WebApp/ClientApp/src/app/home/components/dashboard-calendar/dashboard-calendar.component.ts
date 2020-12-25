import { Component, OnInit } from '@angular/core';

@Component({
  selector: "app-dashboard-calendar",
  templateUrl: "./dashboard-calendar.component.html",
  styleUrls: ["./dashboard-calendar.component.css"],
})
export class DashboardCalendarComponent implements OnInit {
  bsValue = new Date();
  constructor() {}

  ngOnInit(): void {}
}
