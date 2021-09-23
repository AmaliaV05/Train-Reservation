"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Calendar = exports.SeatCalendar = exports.Seat = exports.Car = exports.Type = exports.Train = void 0;
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
var SeatCalendar = /** @class */ (function () {
    function SeatCalendar() {
    }
    return SeatCalendar;
}());
exports.SeatCalendar = SeatCalendar;
var Calendar = /** @class */ (function () {
    function Calendar() {
    }
    return Calendar;
}());
exports.Calendar = Calendar;
//# sourceMappingURL=train.model.js.map