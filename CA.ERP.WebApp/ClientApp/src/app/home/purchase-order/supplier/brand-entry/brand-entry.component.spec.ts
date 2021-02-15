import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandEntryComponent } from './brand-entry.component';

describe('BrandEntryComponent', () => {
  let component: BrandEntryComponent;
  let fixture: ComponentFixture<BrandEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BrandEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BrandEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
