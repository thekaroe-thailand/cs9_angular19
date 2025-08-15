import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportStockComponent } from './stock.component';

describe('ReportStockComponent', () => {
  let component: ReportStockComponent;
  let fixture: ComponentFixture<ReportStockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReportStockComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ReportStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
