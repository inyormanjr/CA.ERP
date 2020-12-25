import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryReceiptViewComponent } from './delivery-receipt-view.component';

describe('DeliveryReceiptViewComponent', () => {
  let component: DeliveryReceiptViewComponent;
  let fixture: ComponentFixture<DeliveryReceiptViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryReceiptViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryReceiptViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
