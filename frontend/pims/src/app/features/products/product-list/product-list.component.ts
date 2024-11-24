import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { ProductCardComponent } from '../product-card/product-card.component';
import { Product } from '../../../models/product.interface';
import { RouterLink, RouterLinkActive, RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-list',
  imports: [ProductCardComponent, RouterModule, RouterLink, RouterLinkActive],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
})
export class ProductListComponent {
  @Input() products?: Product[] | null = null;
  @Output() productSelected = new EventEmitter<string>();

  onProductSelect(id: string): void {
    this.productSelected.emit(id);
  }
}
