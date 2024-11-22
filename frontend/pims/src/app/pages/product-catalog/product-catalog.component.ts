import { Component } from '@angular/core';
import { ProductListComponent } from '../../features/products/product-list/product-list.component';
import { ProductCardComponent } from '../../features/products/product-card/product-card.component';
import { ProductFilterComponent } from '../../shared/components/product-filter/product-filter.component';
import { ProductPreviewComponent } from '../../features/products/product-preview/product-preview.component';

@Component({
  selector: 'app-product-catalog',
  imports: [
    ProductListComponent,
    ProductFilterComponent,
    ProductPreviewComponent,
  ],
  templateUrl: './product-catalog.component.html',
  styleUrl: './product-catalog.component.scss',
})
export class ProductCatalogComponent {}
