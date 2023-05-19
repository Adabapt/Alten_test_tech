import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { ProductService } from 'app/core/services/product.service';
import { Product } from 'app/core/models/product';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  
  layout: string = 'list';
  sortKey:any;
  sortOptions: SelectItem[];
  sortOrder: number;
  sortField: string;

  products: Product[] = [];

  constructor(
    private productService : ProductService,
  ) { }

  ngOnInit(): void {
    this.productService.getAllProduct().then((data) => {
      this.products = data;
    });

    this.sortOptions = [
      { label: 'Price High to Low', value: '!price' },
      { label: 'Price Low to High', value: 'price' },
      { label: 'Rating High to Low', value: '!rating' },
      { label: 'Rating Low to High', value: 'rating' }
    ];
  }

  onSortChange(event) {
    let value = event.value;

    if (value.indexOf('!') === 0) {
        this.sortOrder = -1;
        this.sortField = value.substring(1, value.length);
    } else {
        this.sortOrder = 1;
        this.sortField = value;
    }
  }
}
