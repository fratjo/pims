import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../../core/services/product.service';
import { Bundles, Product } from '../../../models/product.interface';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { AsyncPipe, NgIf } from '@angular/common';
import { BundlePreviewComponent } from '../../bundles/bundle-preview/bundle-preview.component';

@Component({
  selector: 'app-product-preview',
  imports: [AsyncPipe, BundlePreviewComponent],
  templateUrl: './product-preview.component.html',
  styleUrl: './product-preview.component.scss',
})
export class ProductPreviewComponent implements OnInit {
  private readonly productService = inject(ProductService);
  private readonly router = inject(ActivatedRoute);
  public product$: Observable<Product> = new Observable<Product>();
  public bundles$: Observable<Bundles> = new Observable<Bundles>();

  ngOnInit(): void {
    this.router.params.subscribe((params) => {
      const id = params['id'];
      if (id) {
        this.product$ = this.productService.fetchProductById(id);
        this.bundles$ = this.productService.fetchProductBundles(id);
      }
    });
  }
}
