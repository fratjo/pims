import { Component, inject, OnInit } from '@angular/core';
import { ProductListComponent } from '../../features/products/product-list/product-list.component';
import { ProductCardComponent } from '../../features/products/product-card/product-card.component';
import { ProductFilterComponent } from '../../shared/components/product-filter/product-filter.component';
import { ProductPreviewComponent } from '../../features/products/product-preview/product-preview.component';
import { ProductService } from '../../core/services/product.service';
import { Observable } from 'rxjs';
import { Product } from '../../models/product.interface';
import { AsyncPipe } from '@angular/common';
import { FilterPipe } from '../../core/pipes/filter.pipe';
import { RouterLink, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-product-catalog',
  imports: [
    ProductListComponent,
    ProductFilterComponent,
    ProductPreviewComponent,
    AsyncPipe,
    FilterPipe,
    RouterModule,
    RouterLink,
  ],
  templateUrl: './product-catalog.component.html',
  styleUrl: './product-catalog.component.scss',
})
export class ProductCatalogComponent implements OnInit {
  private readonly productService = inject(ProductService);
  products$: Observable<Product[]> = this.productService.products$;
  public search: string = '';

  ngOnInit(): void {
    this.productService.fetchProducts();
  }

  onProductSelected(id: string): void {
    this.productService.setCurrentProduct(id);
  }

  onSearchResultChange(value: string): void {
    this.search = value;
  }
}
