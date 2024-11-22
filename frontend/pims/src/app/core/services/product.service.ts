import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { Product } from '../../models/product.interface';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private readonly httpClient = inject(HttpClient);

  public products$: BehaviorSubject<Product[]> = new BehaviorSubject<Product[]>(
    []
  );

  fetchProducts(): void {
    this.httpClient
      .get<Product[] | []>('http://localhost:5002/api/products')
      .pipe(
        tap((products: Product[]) => {
          this.products$.next(products);
        })
      )
      .subscribe();
  }
}
