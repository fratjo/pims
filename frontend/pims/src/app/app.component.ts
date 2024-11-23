import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ProductCatalogComponent } from './pages/product-catalog/product-catalog.component';
import { NavbarComponent } from './layout/navbar/navbar.component';

@Component({
  selector: 'app-root',
  imports: [NavbarComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'pims';
}
