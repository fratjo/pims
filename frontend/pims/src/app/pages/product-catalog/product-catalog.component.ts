import { Component, inject, OnInit } from '@angular/core';
import { ProductListComponent } from '../../features/products/product-list/product-list.component';
import { ProductFilterComponent } from '../../shared/components/product-filter/product-filter.component';
import { ProductService } from '../../core/services/product.service';
import { Observable } from 'rxjs';
import { Bundles, Product, Products } from '../../models/product.interface';
import { AsyncPipe } from '@angular/common';
import { FilterPipe } from '../../core/pipes/filter.pipe';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-catalog',
  imports: [
    ProductListComponent,
    ProductFilterComponent,
    AsyncPipe,
    FilterPipe,
    RouterModule,
  ],
  providers: [],
  templateUrl: './product-catalog.component.html',
  styleUrl: './product-catalog.component.scss',
})
export class ProductCatalogComponent implements OnInit {
  private readonly productService = inject(ProductService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  products$: Observable<Products | Bundles> = this.productService.products$;
  public search: string = '';

  ngOnInit(): void {
    this.productService.fetchProducts();
  }

  onProductSelected(id: string): void {
    this.router.navigate([id], { relativeTo: this.route });
  }

  onAdding(): void {
    const addActions = document.querySelector('.add-actions');
    if (addActions) addActions.classList.toggle('hidden');
  }

  onProductEdit(): void {
    const currentUrl = this.router.routerState.snapshot.url;
    const currentId = currentUrl.split('/').pop();
    this.router.navigate(['edit', currentId], { relativeTo: this.route });
  }

  onProductAdd(): void {
    this.onAdding();
    this.router.navigate(['add', 'product'], { relativeTo: this.route });
  }

  onBundleAdd(): void {
    this.onAdding();
    this.router.navigate(['add', 'bundle'], { relativeTo: this.route });
  }

  onSearchResultChange(value: string): void {
    this.search = value;
  }
}
