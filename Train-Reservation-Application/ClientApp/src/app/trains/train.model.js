"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Seat = exports.Car = exports.TYPE = exports.Type = exports.Train = void 0;
var Train = /** @class */ (function () {
    function Train() {
    }
    return Train;
}());
exports.Train = Train;
var Type;
(function (Type) {
    Type[Type["All"] = 0] = "All";
    Type[Type["FirstClass"] = 1] = "FirstClass";
    Type[Type["SecondClass"] = 2] = "SecondClass";
    Type[Type["Sleeping"] = 3] = "Sleeping";
})(Type = exports.Type || (exports.Type = {}));
exports.TYPE = ['All', 'First Class', 'Second Class', 'Sleeping'];
var Car = /** @class */ (function () {
    function Car() {
    }
    return Car;
}());
exports.Car = Car;
var Seat = /** @class */ (function () {
    function Seat() {
    }
    return Seat;
}());
exports.Seat = Seat;
//# sourceMappingURL=train-reservation.model.js.map