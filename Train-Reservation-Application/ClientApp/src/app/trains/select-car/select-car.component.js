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
var feature_flags_enum_1 = require("../../core/feature-flags/feature-flags.enum");
var feature_flag_service_1 = require("../../core/feature-flag.service");
var helpers_1 = require("../../core/helpers/helpers");
var enums_1 = require("../enums");
var trains_graphql_service_1 = require("../trains-graphql.service");
var trains_service_1 = require("../trains.service");
var SelectCarComponent = /** @class */ (function () {
    function SelectCarComponent(trainService, trainGraphqlService, featureFlagService) {
        this.trainService = trainService;
        this.trainGraphqlService = trainGraphqlService;
        this.featureFlagService = featureFlagService;
        this.carTypes = [
            { value: enums_1.CarType.FIRST_CLASS, text: 'First Class' },
            { value: enums_1.CarType.SECOND_CLASS, text: 'Second Class' },
            { value: enums_1.CarType.SLEEPING, text: 'Sleeping' }
        ];
        this.filteredCarsByType = new core_1.EventEmitter();
        this.selectCarType = new core_1.EventEmitter();
    }
    SelectCarComponent.prototype.onChangeFilteredCars = function (option) {
        var _this = this;
        this.selectedCarType = option.value;
        if (this.featureFlagService.isEnabled(feature_flags_enum_1.FeatureFlags.UseGraphQL)) {
            this.trainGraphqlService.getCarsByType(this.idTrain, this.selectedDate, this.selectedCarType)
                .subscribe(function (response) {
                var _a;
                _this.filteredCars = (_a = response.data) === null || _a === void 0 ? void 0 : _a.carsByType;
                _this.filteredCarsByType.emit(_this.filteredCars);
                _this.selectCarType.emit(_this.selectedCarType);
            });
        }
        else {
            var selectedDate = (0, helpers_1.getISOStringWithoutTimezone)(this.selectedDate);
            this.trainService.getTrainWithCars(this.idTrain, selectedDate, this.selectedCarType)
                .subscribe(function (response) {
                _this.filteredCars = response;
                _this.filteredCarsByType.emit(_this.filteredCars);
                _this.selectCarType.emit(_this.selectedCarType);
            });
        }
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
    __decorate([
        (0, core_1.Output)(),
        __metadata("design:type", Object)
    ], SelectCarComponent.prototype, "selectCarType", void 0);
    SelectCarComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-select-car',
            templateUrl: './select-car.component.html'
        }),
        __metadata("design:paramtypes", [trains_service_1.TrainsService,
            trains_graphql_service_1.TrainsGraphqlService,
            feature_flag_service_1.FeatureFlagService])
    ], SelectCarComponent);
    return SelectCarComponent;
}());
exports.SelectCarComponent = SelectCarComponent;
//# sourceMappingURL=select-car.component.js.map