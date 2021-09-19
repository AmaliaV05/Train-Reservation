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
    Type["All"] = "All";
    Type["FirstClass"] = "First Class";
    Type["SecondClass"] = "Second Class";
    Type["Sleeping"] = "Slepping";
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
//# sourceMappingURL=train.model.js.map