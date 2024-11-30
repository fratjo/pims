import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import {
  Bundles,
  Product,
  ProductId,
  Products,
} from '../../models/product.interface';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private readonly httpClient = inject(HttpClient);
  public products$: BehaviorSubject<Products> = new BehaviorSubject<Products>(
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

  fetchProductBundles(id: string): Observable<Bundles> {
    return this.httpClient.get<Bundles>(
      `http://localhost:5002/api/products/${id}/bundles`
    );
  }

  postProduct(product: Product): Observable<ProductId> {
    return this.httpClient
      .post<ProductId>('http://localhost:5002/api/products', product)
      .pipe(
        tap((newId) => {
          product.id = newId.id;
          product.name =
            product.name!.charAt(0).toUpperCase() +
            product.name!.slice(1).toLowerCase();
          product.category =
            product.category!.charAt(0).toUpperCase() +
            product.category!.slice(1).toLowerCase();
          this.products$.next([...this.products$.value, product]);
        })
      );
  }

  putProduct(product: Product): Observable<Product> {
    return this.httpClient
      .put<Product>(`http://localhost:5002/api/products/${product.id}`, product)
      .pipe(
        tap((product) => {
          this.products$.next(
            this.products$.value.map((p) => (p.id === product.id ? product : p)) // Update the product in the list
          );
        })
      );
  }
}
