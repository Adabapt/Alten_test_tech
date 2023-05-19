import { Component, OnInit } from '@angular/core';
import { ProductService } from 'app/core/services/product.service';
import { Product } from 'app/core/models/product';

@Component({
  selector: 'app-products-admin',
  templateUrl: './products-admin.component.html',
  styleUrls: ['./products-admin.component.scss']
})
export class ProductsAdminComponent implements OnInit {

  selectedProducts: Product[] =[];

  products: Product[] = [];

  productForm: Product = new Product();

  formAff = "";

  constructor(
    private productService : ProductService,
  ) { }

  async ngOnInit() {
    await this.refreshProducts();
  }

  async refreshProducts(){
    await this.productService.getAllProduct().then((data) => {
      this.products = data;
    });
  }

  async onClickDelete(product:Product)
  {
    await this.productService.deleteProduct(product.id);
    await this.refreshProducts();
  }

  async onClickDeleteSelection()
  {
    for(var i=0;i<this.selectedProducts.length;i++)
    {
      await this.productService.deleteProduct(this.selectedProducts[i].id);
    }
    await this.refreshProducts();
  }

  onClickCreation(){
    this.productForm = new Product();
    this.formAff = "Creation";
  }

  onClickCancel(){
    this.formAff = "";
  }

  async onClickValidate(){
    if(this.productForm.code != undefined && this.productForm.name != undefined && this.productForm.description != undefined 
      && this.productForm.price != undefined && this.productForm.quantity != undefined && this.productForm.inventoryStatus != undefined 
      && this.productForm.category != undefined  )
    {
      await this.productService.insertProduct(this.productForm);
      this.productForm = new Product();
      this.formAff = "";
      await this.refreshProducts();
    }
  }

  onClickModify(product:Product)
  {
    this.formAff = 'Modif';
    this.productForm = product;
  }

  async onClickUpdate(){
    if(this.productForm.code != undefined && this.productForm.name != undefined && this.productForm.description != undefined 
      && this.productForm.price != undefined && this.productForm.quantity != undefined && this.productForm.inventoryStatus != undefined 
      && this.productForm.category != undefined  )
    {
      await this.productService.updateProduct(this.productForm);
      this.productForm = new Product();
      this.formAff = "";
      await this.refreshProducts();
    }
  }

}
