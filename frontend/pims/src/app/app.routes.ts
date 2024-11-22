import { Routes } from '@angular/router';
import { ProductCatalogComponent } from './pages/product-catalog/product-catalog.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'catalog',
    pathMatch: 'full',
  },
  {
    path: 'catalog',
    component: ProductCatalogComponent,
  },
  {
    path: '**',
    redirectTo: 'catalog',
  },
];
