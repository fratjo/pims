import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ProductService } from '../../core/services/product.service';
import { Router } from '@angular/router';
import { Product } from '../../models/product.interface';

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

  public productForm!: FormGroup;

  ngOnInit(): void {
    this.productForm = this.initForm();
  }

  private initForm(
    product: Product = {
      name: '',
      description: '',
      category: '',
      price: 0,
      stockQuantity: 0,
    }
  ): FormGroup {
    return this.fb.group({
      name: [product.name, Validators.required],
      category: [product.category, Validators.required],
      description: product.description,
      price: [product.price, [Validators.required, Validators.min(0.01)]],
      stockQuantity: [
        product.stockQuantity,
        [Validators.required, Validators.min(0)],
      ],
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      this.productService.postProduct(this.productForm.value);
      this.router.navigate(['/catalog']);
    } else {
      console.log('Form invalid');
    }
  }
}
