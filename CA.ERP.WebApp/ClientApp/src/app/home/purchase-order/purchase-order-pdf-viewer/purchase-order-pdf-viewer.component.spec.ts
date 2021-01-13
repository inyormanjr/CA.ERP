import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseOrderPdfViewerComponent } from './purchase-order-pdf-viewer.component';

describe('PurchaseOrderPdfViewerComponent', () => {
  let component: PurchaseOrderPdfViewerComponent;
  let fixture: ComponentFixture<PurchaseOrderPdfViewerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PurchaseOrderPdfViewerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PurchaseOrderPdfViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
