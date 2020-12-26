import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersManagementViewComponent } from './users-management-view.component';

describe('UsersManagementViewComponent', () => {
  let component: UsersManagementViewComponent;
  let fixture: ComponentFixture<UsersManagementViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersManagementViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersManagementViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
