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
var forms_1 = require("@angular/forms");
var core_2 = require("@angular/material/core");
var feature_flag_service_1 = require("../../core/feature-flag.service");
var feature_flags_enum_1 = require("../../core/feature-flags/feature-flags.enum");
var helpers_1 = require("../../core/helpers/helpers");
var data_service_1 = require("../data.service");
var trains_graphql_service_1 = require("../trains-graphql.service");
var trains_service_1 = require("../trains.service");
var DateFilterComponent = /** @class */ (function () {
    function DateFilterComponent(trainService, trainGraphqlService, featureFlagService, dataService, _adapter) {
        this.trainService = trainService;
        this.trainGraphqlService = trainGraphqlService;
        this.featureFlagService = featureFlagService;
        this.dataService = dataService;
        this._adapter = _adapter;
        this.search = new forms_1.FormControl('');
        this._adapter.setLocale('ro');
        this.minDate = new Date();
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
        this.selectedValue = this.search.value;
        this.selectedDate = this.selectedValue.toDate();
        if (this.featureFlagService.isEnabled(feature_flags_enum_1.FeatureFlags.UseGraphQL)) {
            var selectedDate = this.selectedValue.toDate();
            var selectedDay = selectedDate.getDay();
            this.trainGraphqlService.getTrainsByDate(selectedDay).subscribe(function (response) {
                var _a;
                _this.trains = (_a = response.data) === null || _a === void 0 ? void 0 : _a.trainsByDate;
            });
        }
        else {
            var selectedDate = (0, helpers_1.getISOStringWithoutTimezone)(this.selectedDate);
            this.trainService.getTrainList(selectedDate).subscribe(function (response) {
                return _this.trains = response;
            });
        }
        this.dataService.getReservationDate(this.selectedDate);
    };
    DateFilterComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-date-filter',
            templateUrl: './date-filter.component.html'
        }),
        __metadata("design:paramtypes", [trains_service_1.TrainsService,
            trains_graphql_service_1.TrainsGraphqlService,
            feature_flag_service_1.FeatureFlagService,
            data_service_1.DataService,
            core_2.DateAdapter])
    ], DateFilterComponent);
    return DateFilterComponent;
}());
exports.DateFilterComponent = DateFilterComponent;
//# sourceMappingURL=date-filter.component.js.map