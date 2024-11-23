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
  public currentProductPreview$: BehaviorSubject<Product> =
    new BehaviorSubject<Product>({});

  // HTTP CALLS

  fetchProducts(): void {
    this.httpClient
      .get<Product[] | []>('http://localhost:5002/api/products')
      .pipe(
        tap((products: Product[]) => {
          this.products$.next(products);
          this.currentProductPreview$.next(products[0] ?? {});
        })
      )
      .subscribe();
  }

  postProduct(product: Product): void {
    this.httpClient
      .post<string>('http://localhost:5002/api/products', product)
      .pipe(
        tap((newId) => {
          product.id = newId;
          this.products$.next([...this.products$.value, product]);
        })
      )
      .subscribe();
  }

  // NON HTTP CALLS

  setCurrentProduct(id: string): void {
    const products = this.products$.getValue();
    const selectedProduct = products.find((product) => product.id === id);

    if (selectedProduct) this.currentProductPreview$.next(selectedProduct);
    else this.currentProductPreview$.next(products[0] ?? {});
  }
}
