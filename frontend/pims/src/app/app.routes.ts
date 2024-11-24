import { Routes } from '@angular/router';
import { ProductCatalogComponent } from './pages/product-catalog/product-catalog.component';
import { ProductPreviewComponent } from './features/products/product-preview/product-preview.component';
import { ProductAddFormsComponent } from './pages/product-add-forms/product-add-forms.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'catalog',
    pathMatch: 'full',
  },
  {
    path: 'catalog',
    component: ProductCatalogComponent,
    children: [
      {
        path: ':id',
        component: ProductPreviewComponent,
      },
    ],
  },
  {
    path: 'products/add',
    component: ProductAddFormsComponent,
  },
  {
    path: 'products/edit/:id',
    component: ProductAddFormsComponent,
  },
  {
    path: '**',
    redirectTo: 'catalog',
  },
];
