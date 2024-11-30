import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BundlePreviewComponent } from './bundle-preview.component';

describe('BundlePreviewComponent', () => {
  let component: BundlePreviewComponent;
  let fixture: ComponentFixture<BundlePreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BundlePreviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BundlePreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
