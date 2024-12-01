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
import { Bundle, Product, Products } from '../../../models/product.interface';
import { Location, NgFor } from '@angular/common';
import { fromEvent } from 'rxjs';

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
      price: [bundle.price, [Validators.required, Validators.min(0)]],
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
    const minPrice =
      0.6 *
      this.productList
        .filter((product: Product) => {
          return this.bundleForm.value.products?.some(
            (p: Product) => p.id === product.id
          );
        })
        .reduce((acc, product) => {
          return acc + product.price!;
        }, 0);

    const maxPrice =
      0.95 *
      this.productList
        .filter((product: Product) => {
          return this.bundleForm.value.products?.some(
            (p: Product) => p.id === product.id
          );
        })
        .reduce((acc, product) => {
          return acc + product.price!;
        }, 0);

    if (
      this.bundleForm.value.price < minPrice ||
      this.bundleForm.value.price > maxPrice
    ) {
      const errorMessage = document.createElement('div');
      errorMessage.innerText = `Price must be between ${minPrice} and ${maxPrice}`;
      errorMessage.style.color = 'red';
      const formElement = document.querySelector('.error-message');
      formElement!.innerHTML = '';
      formElement!.appendChild(errorMessage);
      return;
    }

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
            const errorMessage = document.createElement('div');
            errorMessage.innerText = err.error.message;
            errorMessage.style.color = 'red';
            const formElement = document.querySelector('form');
            formElement?.appendChild(errorMessage);
          },
        });
        this.router.navigate(['/catalog']);
      }
    }
  }
}
