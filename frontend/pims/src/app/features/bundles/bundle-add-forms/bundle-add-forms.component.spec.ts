import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BundleAddFormsComponent } from './bundle-add-forms.component';

describe('BundleAddFormsComponent', () => {
  let component: BundleAddFormsComponent;
  let fixture: ComponentFixture<BundleAddFormsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BundleAddFormsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BundleAddFormsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
