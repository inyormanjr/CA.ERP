import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PoEntryComponent } from './po-entry.component';

describe('PoEntryComponent', () => {
  let component: PoEntryComponent;
  let fixture: ComponentFixture<PoEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PoEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PoEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
