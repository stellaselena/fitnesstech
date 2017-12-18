"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Order = /** @class */ (function () {
    function Order() {
        this.orderDate = new Date();
        this.items = new Array();
        //get subtotal(): number {
        //    return _.sum(_.map(this.items, i => i.unitPrice * i.quantity));
        //};
    }
    return Order;
}());
exports.Order = Order;
var OrderItem = /** @class */ (function () {
    function OrderItem() {
    }
    return OrderItem;
}());
exports.OrderItem = OrderItem;
//# sourceMappingURL=order.js.map