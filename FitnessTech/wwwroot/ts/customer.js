var Customer = /** @class */ (function () {
    function Customer(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.visits = 0;
    }
    Customer.prototype.showName = function () {
        alert(this.firstName + " " + this.lastName);
    };
    Object.defineProperty(Customer.prototype, "name", {
        get: function () {
            return this.ourName;
        },
        set: function (val) {
            this.ourName = val;
        },
        enumerable: true,
        configurable: true
    });
    return Customer;
}());
//# sourceMappingURL=customer.js.map