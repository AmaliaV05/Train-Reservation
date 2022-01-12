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
exports.DateFilterComponent = void 0;
var core_1 = require("@angular/core");
var trains_service_1 = require("../trains.service");
var core_2 = require("@angular/material/core");
var forms_1 = require("@angular/forms");
var data_service_1 = require("../data.service");
var DateFilterComponent = /** @class */ (function () {
    function DateFilterComponent(trainService, dataService, _adapter) {
        this.trainService = trainService;
        this.dataService = dataService;
        this._adapter = _adapter;
        this.search = new forms_1.FormControl('');
        this._adapter.setLocale('ro');
    }
    DateFilterComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.dataService.currentMessage$.subscribe(function (message) {
            return _this.selectedDate = message;
        });
    };
    DateFilterComponent.prototype.ngOnDestroy = function () {
        this.subscription.unsubscribe();
    };
    DateFilterComponent.prototype.getTrainList = function () {
        var _this = this;
        this.selectedDate = this.search.value.toISOString().split('T')[0];
        this.trainService.getTrainList(this.selectedDate).subscribe(function (response) {
            return _this.trains = response;
        });
        this.dataService.getReservationDate(this.selectedDate);
    };
    DateFilterComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-date-filter',
            templateUrl: './date-filter.component.html'
        }),
        __metadata("design:paramtypes", [trains_service_1.TrainsService,
            data_service_1.DataService,
            core_2.DateAdapter])
    ], DateFilterComponent);
    return DateFilterComponent;
}());
exports.DateFilterComponent = DateFilterComponent;
//# sourceMappingURL=date-filter.component.js.map