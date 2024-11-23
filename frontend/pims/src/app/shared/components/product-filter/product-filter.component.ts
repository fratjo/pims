import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-filter',
  imports: [FormsModule],
  templateUrl: './product-filter.component.html',
  styleUrl: './product-filter.component.scss',
})
export class ProductFilterComponent {
  @Output() searchResult = new EventEmitter<string>();
  public search: string = '';

  onSearchChange(): void {
    this.searchResult.emit(this.search);
  }
}
