import { Component, inject, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ProductService } from '../../../core/services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Bundle, Products } from '../../../models/product.interface';
import { Location, NgFor } from '@angular/common';

@Component({
  selector: 'app-bundle-add-forms',
  imports: [FormsModule, ReactiveFormsModule, NgFor],
  standalone: true,
  templateUrl: './bundle-add-forms.component.html',
  styleUrl: './bundle-add-forms.component.scss',
})
export class BundleAddFormsComponent implements OnInit {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private location = inject(Location);
  public productList: Products = this.productService.products$.value;
  public bundleForm: FormGroup = this.initForm();

  get products(): FormArray {
    return this.bundleForm.get('products') as FormArray;
  }

  ngOnInit(): void {
    this.productService.fetchProducts();
  }

  private initForm(
    bundle: Bundle = {
      id: '',
      name: '',
      description: '',
      price: 0,
      products: [],
    }
  ): FormGroup {
    return this.fb.group({
      name: [bundle.name, Validators.required],
      description: [bundle.description],
      price: [bundle.price, Validators.required],
      products: this.fb.array(
        bundle.products!.map((product) => {
          return this.fb.group({
            id: [product.id, Validators.required],
          });
        }),
        Validators.required
      ),
    });
  }

  addProduct(): void {
    this.products.push(
      this.fb.group({
        id: ['', Validators.required],
      })
    );
  }

  removeProduct(index: number): void {
    this.products.removeAt(index);
  }

  goBack(): void {
    this.location.back();
  }

  onSubmit(): void {
    if (this.bundleForm.valid) {
      const formValue = this.bundleForm.value;
      const payload = {
        name: formValue.name,
        description: formValue.description,
        price: formValue.price,
        products: formValue.products.map(
          (product: { id: string }) => product.id
        ),
      };
      if (formValue.id) {
        // this.productService.updateBundle(bundle).subscribe(() => {
        //   this.router.navigate(['/catalog']);
        // });
      } else {
        this.productService.postBundle(payload).subscribe({
          next: (id) => {
            console.log(id);
          },
          error: (err) => {
            console.error('Error posting bundle:', err);
          },
        });
        this.router.navigate(['/catalog']);
      }
    }
  }
}
