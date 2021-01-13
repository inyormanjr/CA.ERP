import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierSelectionModalComponent } from './supplier-selection-modal.component';

describe('SupplierSelectionModalComponent', () => {
  let component: SupplierSelectionModalComponent;
  let fixture: ComponentFixture<SupplierSelectionModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierSelectionModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierSelectionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
