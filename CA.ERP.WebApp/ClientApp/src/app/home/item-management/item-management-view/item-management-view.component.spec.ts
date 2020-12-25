import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemManagementViewComponent } from './item-management-view.component';

describe('ItemManagementViewComponent', () => {
  let component: ItemManagementViewComponent;
  let fixture: ComponentFixture<ItemManagementViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemManagementViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemManagementViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
