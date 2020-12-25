import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardCalendarComponent } from './dashboard-calendar.component';

describe('DashboardCalendarComponent', () => {
  let component: DashboardCalendarComponent;
  let fixture: ComponentFixture<DashboardCalendarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardCalendarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
