import { Pipe, PipeTransform } from '@angular/core';
import { Product } from '../../models/product.interface';

@Pipe({
  name: 'filter',
  pure: false,
})
export class FilterPipe implements PipeTransform {
  transform(value: Product[] | null, search: string): Product[] {
    if (!Array.isArray(value)) {
      return []; // si le tableau n'existe pas
    }
    if (!search.trim()) {
      return value; // si search est vide
    }
    return value.filter((product) => {
      return product.name?.toLowerCase().includes(search.toLowerCase());
    });
  }
}
