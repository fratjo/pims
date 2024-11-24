import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../../core/services/product.service';
import { Observable } from 'rxjs';
import { Product } from '../../../models/product.interface';
import { AsyncPipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-preview',
  imports: [AsyncPipe],
  templateUrl: './product-preview.component.html',
  styleUrl: './product-preview.component.scss',
})
export class ProductPreviewComponent implements OnInit {
  private readonly productService = inject(ProductService);
  private readonly router = inject(ActivatedRoute);
  public product: Product | undefined = {};

  ngOnInit(): void {
    this.router.params.subscribe((params) => {
      const id = params['id'];
      this.product = this.productService.products$.value.find(
        (product) => product.id === id
      );
    });
  }
}
