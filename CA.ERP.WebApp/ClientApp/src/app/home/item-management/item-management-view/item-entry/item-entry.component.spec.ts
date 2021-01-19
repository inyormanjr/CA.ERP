import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemEntryComponent } from './item-entry.component';

describe('ItemEntryComponent', () => {
  let component: ItemEntryComponent;
  let fixture: ComponentFixture<ItemEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
