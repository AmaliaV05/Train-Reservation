"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.MultipleSeatsComponent = void 0;
var core_1 = require("@angular/core");
var trains_service_1 = require("../trains.service");
var MultipleSeatsComponent = /** @class */ (function () {
    function MultipleSeatsComponent(trainService) {
        this.trainService = trainService;
        this.selectedGroupSeatsNumber = new core_1.EventEmitter();
        this.selectedGroupSeatsList = new core_1.EventEmitter();
        this.multipleSeats = [
            { value: 1, text: '1' },
            { value: 2, text: '2' },
            { value: 3, text: '3' },
            { value: 4, text: '4' },
            { value: 5, text: '5' },
            { value: 6, text: '6' },
            { value: 7, text: '7' }
        ];
    }
    MultipleSeatsComponent.prototype.onChangeMultipleSeatsNumber = function (option) {
        var _this = this;
        this.N = option.value;
        this.trainService.getTrainWithMultipleAvailableSeats(this.idTrain, this.selectedDate, this.N)
            .subscribe(function (response) {
            _this.availableGroupSeatIds = response;
            _this.selectedGroupSeatsNumber.emit(_this.N);
            _this.selectedGroupSeatsList.emit(_this.availableGroupSeatIds);
        });
    };
    __decorate([
        (0, core_1.Input)(),
        __metadata("design:type", Number)
    ], MultipleSeatsComponent.prototype, "idTrain", void 0);
    __decorate([
        (0, core_1.Input)(),
        __metadata("design:type", Date)
    ], MultipleSeatsComponent.prototype, "selectedDate", void 0);
    __decorate([
        (0, core_1.Output)(),
        __metadata("design:type", Object)
    ], MultipleSeatsComponent.prototype, "selectedGroupSeatsNumber", void 0);
    __decorate([
        (0, core_1.Output)(),
        __metadata("design:type", Object)
    ], MultipleSeatsComponent.prototype, "selectedGroupSeatsList", void 0);
    MultipleSeatsComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-multiple-seats',
            templateUrl: './multiple-seats.component.html'
        }),
        __metadata("design:paramtypes", [trains_service_1.TrainsService])
    ], MultipleSeatsComponent);
    return MultipleSeatsComponent;
}());
exports.MultipleSeatsComponent = MultipleSeatsComponent;
//# sourceMappingURL=multiple-seats.component.js.map