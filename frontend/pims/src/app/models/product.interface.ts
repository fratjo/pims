export interface Product {
  id?: string;
  name?: string;
  category?: string;
  description?: string;
  price?: number;
  stockQuantity?: number;
}
export type Products = Product[];

export interface ProductId {
  id: string;
}

export interface Bundle {
  id?: string;
  name?: string;
  description?: string;
  price?: number;
  products?: Products;
}
export type Bundles = Bundle[];
