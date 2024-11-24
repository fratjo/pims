import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
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

  fetchProductById(id: string): Observable<Product> {
    return this.httpClient.get<Product>(
      `http://localhost:5002/api/products/${id}`
    );
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

  putProduct(product: Product): void {
    this.httpClient
      .put(`http://localhost:5002/api/products/${product.id}`, product)
      .pipe(
        tap(() => {
          this.products$.next(
            this.products$.value.map((p) => (p.id === product.id ? product : p)) // Update the product in the list
          );
        })
      )
      .subscribe();
  }
}
