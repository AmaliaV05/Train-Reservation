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
exports.SelectCarComponent = void 0;
var core_1 = require("@angular/core");
var train_model_1 = require("../train.model");
var trains_service_1 = require("../trains.service");
var SelectCarComponent = /** @class */ (function () {
    function SelectCarComponent(trainService) {
        this.trainService = trainService;
        this.carTypes = [
            { value: train_model_1.CarType.FirstClass, text: 'First Class' },
            { value: train_model_1.CarType.SecondClass, text: 'Second Class' },
            { value: train_model_1.CarType.Sleeping, text: 'Sleeping' }
        ];
        this.filteredCarsByType = new core_1.EventEmitter();
    }
    SelectCarComponent.prototype.onChangeFilteredCars = function (option) {
        var _this = this;
        this.selectedCarType = option.value;
        this.trainService.getTrainWithCars(this.idTrain, this.selectedDate, this.selectedCarType)
            .subscribe(function (response) {
            _this.filteredCars = response;
            _this.filteredCarsByType.emit(_this.filteredCars);
        });
    };
    __decorate([
        (0, core_1.Input)(),
        __metadata("design:type", Number)
    ], SelectCarComponent.prototype, "idTrain", void 0);
    __decorate([
        (0, core_1.Input)(),
        __metadata("design:type", Date)
    ], SelectCarComponent.prototype, "selectedDate", void 0);
    __decorate([
        (0, core_1.Output)(),
        __metadata("design:type", Object)
    ], SelectCarComponent.prototype, "filteredCarsByType", void 0);
    SelectCarComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-select-car',
            templateUrl: './select-car.component.html'
        }),
        __metadata("design:paramtypes", [trains_service_1.TrainsService])
    ], SelectCarComponent);
    return SelectCarComponent;
}());
exports.SelectCarComponent = SelectCarComponent;
//# sourceMappingURL=select-car.component.js.map