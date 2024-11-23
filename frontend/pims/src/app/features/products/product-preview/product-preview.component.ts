import { Component, inject } from '@angular/core';
import { ProductService } from '../../../core/services/product.service';
import { Observable } from 'rxjs';
import { Product } from '../../../models/product.interface';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-product-preview',
  imports: [AsyncPipe],
  templateUrl: './product-preview.component.html',
  styleUrl: './product-preview.component.scss',
})
export class ProductPreviewComponent {
  private readonly productService = inject(ProductService);
  currentProductPreview$: Observable<Product> =
    this.productService.currentProductPreview$;
}
