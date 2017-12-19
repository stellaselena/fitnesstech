import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Router } from "@angular/router";

@Component({
    selector: "checkout",
    templateUrl: "checkout.component.html",
    styleUrls: ['checkout.component.css']
})
export class Checkout {

    constructor(public data: DataService, public router: Router) {
    }

    errorMessage: string = "";
    successMessage: string = "";

    onCheckout() {
        this.data.checkout().subscribe(success => {
            if (success) {
                //this.router.navigate(["/"]);
                this.successMessage = "Order submitted successfully!";
            }
        },
            err => this.errorMessage = "Failed to save order");
    }
}
