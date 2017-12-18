import { Http, Response, Headers } from "@angular/http";
import { Observable } from "rxjs";
import { Product } from "./product";
import 'rxjs/add/operator/map';
import { Injectable } from "@angular/core";
import { Order, OrderItem } from "./order";


@Injectable()
export class DataService {
    constructor(private http: Http) {

    }
    public order: Order = new Order();
    public products: Product[] = [];

    public loadProducts(): Observable<Product[]> {
        return this.http.get("/api/products")
            .map((result: Response) => this.products = result.json());
    }

    public AddToOrder(product: Product) {

        let item: OrderItem = this.order.items.find(i => i.productId == product.id);
        if (item) {
            item.quantity++;
        } else {
            item = new OrderItem;
            item.productId = product.id;
            item.productProducer = product.producer;
            item.productCategory = product.category;
            item.productSize = product.size;
            item.unitPrice = product.price;
            item.productTitle = product.title;
            item.productSupplementId = product.supplementId;
            item.quantity = 1;
            this.order.items.push(item);
        }

    }
}
