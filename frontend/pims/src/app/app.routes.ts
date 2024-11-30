import { Routes } from '@angular/router';
import { ProductCatalogComponent } from './pages/product-catalog/product-catalog.component';
import { ProductPreviewComponent } from './features/products/product-preview/product-preview.component';
import { ProductAddFormsComponent } from './features/products/product-add-forms/product-add-forms.component';
import { BundleAddFormsComponent } from './features/bundles/bundle-add-forms/bundle-add-forms.component';

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
        path: 'add',
        children: [
          {
            path: 'product',
            component: ProductAddFormsComponent,
          },
          {
            path: 'bundle',
            component: BundleAddFormsComponent,
          },
        ],
      },
      {
        path: ':id',
        component: ProductPreviewComponent,
      },
      {
        path: 'edit/:id',
        component: ProductAddFormsComponent,
      },
    ],
  },
  {
    path: '**',
    redirectTo: 'catalog',
  },
];
