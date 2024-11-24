import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ProductService } from '../../core/services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../../models/product.interface';
import { first } from 'rxjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-product-add-forms',
  imports: [FormsModule, ReactiveFormsModule],
  standalone: true,
  templateUrl: './product-add-forms.component.html',
  styleUrl: './product-add-forms.component.scss',
})
export class ProductAddFormsComponent implements OnInit {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private location = inject(Location);
  public action: string = 'Add';
  private product: Product | null = null;

  public productForm: FormGroup = this.initForm();

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id !== null) {
        this.action = 'Edit';
        this.productService
          .fetchProductById(id)
          .pipe(first((p) => !!p))
          .subscribe((product) => {
            this.productForm = this.initForm(product);
          });
      } else {
        this.productForm = this.initForm();
      }
    });
  }

  private initForm(
    product: Product = {
      id: '',
      name: '',
      description: '',
      category: '',
      price: 0,
      stockQuantity: 0,
    }
  ): FormGroup {
    this.product = product.id ? product : null;

    return this.fb.group({
      name: [product.name, this.action === 'Edit' ? [] : Validators.required],
      category: [
        product.category,
        this.action === 'Edit' ? [] : Validators.required,
      ],
      description: product.description,
      price: [
        product.price,
        this.action === 'Edit'
          ? []
          : [Validators.required, Validators.min(0.01)],
      ],
      stockQuantity: [
        product.stockQuantity,
        this.action === 'Edit' ? [] : [Validators.required, Validators.min(0)],
      ],
    });
  }

  goBack(): void {
    this.location.back();
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      if (this.action === 'Add') {
        this.productService.postProduct(this.productForm.value).subscribe({
          next: (id) => {
            this.router.navigate(['/catalog', id.id]);
          },
          error: (err: any) => {
            const errorMessage = document.createElement('div');
            errorMessage.innerText = err.error.message;
            errorMessage.style.color = 'red';
            const formElement = document.querySelector('form');
            formElement?.appendChild(errorMessage);
          },
        });
      }
      if (this.action === 'Edit') {
        this.productService
          .putProduct({
            ...this.productForm.value,
            id: this.product?.id,
          })
          .subscribe({
            next: (product) => this.router.navigate(['/catalog', product.id]),
            error: (err) => {
              const errorMessage = document.createElement('div');
              errorMessage.innerText = err.error.message;
              errorMessage.style.color = 'red';
              const formElement = document.querySelector('form');
              formElement?.appendChild(errorMessage);
            },
          });
      }
    }
  }
}
