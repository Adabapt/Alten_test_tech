import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom, lastValueFrom } from 'rxjs';

import { ConfigurationService } from './configuration.service';
import { Product } from '../models/product';

@Injectable({
    providedIn: 'root',
})

export class ProductService {
    product : Product = new Product();
    tabProduct : Array<Product> = new Array<Product>();

    /**
     * Constructeur du service (ref vers le client http pour faire les requetes et le service de configuration pour accèder à l'API configurée)
     */
    constructor(
        private httpClient: HttpClient,
        public configurationService: ConfigurationService
      ) {}


    /**
     * source : BASE DE DONNEES
     * requete : get
     * parametre :
     * return : liste des produits
     * resume : renvoie tous les produits enregistrés en base
     */
    async getAllProduct()
    {
        await this.configurationService.getConfiguration();
        await lastValueFrom(this.httpClient.get(
            this.configurationService.configuration.url_api + 
            this.configurationService.configuration.url_product +
            this.configurationService.configuration.url_get + "/Products"
        )).then((data) => {
            this.tabProduct = <Product[]>data;
            console.debug(JSON.stringify(this.tabProduct));
        })
        .catch((error) => {
            console.error(error);
        })
        return this.tabProduct;
    }

    /**
     * source : BASE DE DONNEES
     * requete : get
     * parametre : id du product
     * return : le product récupéré
     * resume : interroge la base de données pour récupérer les données d'un product en fonction de son id
     */
    async getProduct(idProduct : number)
    {
        await this.configurationService.getConfiguration();
        await firstValueFrom(this.httpClient.get(
            this.configurationService.configuration.url_api + 
            this.configurationService.configuration.url_product +
            this.configurationService.configuration.url_get + "/Products/Id" +
            "?idProduct=" + idProduct
        )).then((data) => {
            this.product = <Product>data;
            console.debug(JSON.stringify(this.product));
        })
        .catch((error) => {
            console.error(error);
        })
        return this.product;
    }

    /**
     * source : BASE DE DONNEES
     * requete : insert
     * parametre : données du produit à ajouter
     * return : message de retour
     * resume : ajoute un produit à la base
     */
    async insertProduct(product: Product)
    {
        await this.configurationService.getConfiguration();
        await firstValueFrom(this.httpClient.post(
            this.configurationService.configuration.url_api + 
            this.configurationService.configuration.url_product +
            this.configurationService.configuration.url_post + "/Products",
            {
                code: product.code,
                name: product.name,
                description: product.description,
                price: product.price,
                quantity: product.quantity,
                inventoryStatus: product.inventoryStatus,
                category: product.category,
                image: product.image,
                rating: product.rating
            }
        )).then((data:any) => {
            if(data.status == "KO")
            {
                console.error(data);
            }
        })
        .catch((error) => {
            console.error(error);
        })
    }

    /**
     * source : BASE DE DONNEES
     * requete : patch
     * parametre : id du product et données du product
     * return : message de retour
     * resume : met à jour les données d'un produit en base selon son id
     */
    async updateProduct(product: Product)
    {
        await this.configurationService.getConfiguration();
        await firstValueFrom(this.httpClient.patch(
            this.configurationService.configuration.url_api + 
            this.configurationService.configuration.url_product +
            this.configurationService.configuration.url_patch + "/Products",
            {
                id: product.id,
                code: product.code,
                name: product.name,
                description: product.description,
                price: product.price,
                quantity: product.quantity,
                inventoryStatus: product.inventoryStatus,
                category: product.category,
                image: product.image,
                rating: product.rating
            }
        )).then((data:any) => {
            if(data.status == "KO")
            {
                console.error(data);
            }
        })
        .catch((error) => {
            console.error(error);
        })
    }

    /**
     * source : BASE DE DONNEES
     * requete : delete
     * parametre : id du product
     * return : message de retour
     * resume : supprime un produit de la base en fonction de son id
     */
    async deleteProduct(idProduct: number)
    {
        await this.configurationService.getConfiguration();
        await firstValueFrom(this.httpClient.delete(
            this.configurationService.configuration.url_api + 
            this.configurationService.configuration.url_product +
            this.configurationService.configuration.url_del + "/Products/Id" +
            "?idProduct=" + idProduct,
        )).then((data:any) => {
            if(data.status == "KO")
            {
                console.error(data);
            }
        })
        .catch((error) => {
            console.error(error);
        })
    }
}