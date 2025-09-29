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
exports.CarListComponent = void 0;
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var feature_flag_service_1 = require("../../core/feature-flag.service");
var feature_flags_enum_1 = require("../../core/feature-flags/feature-flags.enum");
var helpers_1 = require("../../core/helpers/helpers");
var data_service_1 = require("../data.service");
var enums_1 = require("../enums");
var trains_graphql_service_1 = require("../trains-graphql.service");
var trains_service_1 = require("../trains.service");
var SelectedSeatsView = /** @class */ (function () {
    function SelectedSeatsView() {
    }
    return SelectedSeatsView;
}());
var CarListComponent = /** @class */ (function () {
    function CarListComponent(trainService, trainGraphqlService, featureFlagService, route, router, dataService) {
        this.trainService = trainService;
        this.trainGraphqlService = trainGraphqlService;
        this.featureFlagService = featureFlagService;
        this.route = route;
        this.router = router;
        this.dataService = dataService;
        this.selectedTrain = {
            id: 0,
            name: '',
            cars: [{
                    id: 0,
                    carNumber: 0,
                    numberOfSeats: 0,
                    type: enums_1.CarType.ALL,
                    seats: [{
                            id: 0,
                            number: 0,
                            seatCalendars: [{
                                    seatAvailability: false
                                }]
                        }]
                }]
        };
        this.selectedCarType = enums_1.CarType.ALL;
        this.N = 1;
        this.reserveSeatsView = new Array();
        this.reserveSeatView = { car: 0, seat: 0 };
        this.reserveSeatsIds = new Array();
        this.reserveButton = true;
    }
    CarListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.dataService.currentMessage$.subscribe(function (message) {
            return _this.selectedDate = message;
        });
        this.seatsListSubscription = this.dataService.currentSeatListMessage$.subscribe(function (message) {
            return _this.reserveSeatsIds = message;
        });
        this.idTrain = parseInt(this.route.snapshot.params['id']);
        this.getSelectedTrain();
    };
    CarListComponent.prototype.ngOnDestroy = function () {
        this.subscription.unsubscribe();
        this.seatsListSubscription.unsubscribe();
    };
    CarListComponent.prototype.getSelectedTrain = function () {
        var _this = this;
        if (this.featureFlagService.isEnabled(feature_flags_enum_1.FeatureFlags.UseGraphQL)) {
            this.trainGraphqlService.getCarsByType(this.idTrain, this.selectedDate, this.selectedCarType)
                .subscribe(function (response) {
                var _a;
                _this.selectedTrain = (_a = response.data) === null || _a === void 0 ? void 0 : _a.carsByType;
            });
        }
        else {
            var selectedDate = (0, helpers_1.getISOStringWithoutTimezone)(this.selectedDate);
            this.trainService.getTrainWithCars(this.idTrain, selectedDate, this.selectedCarType)
                .subscribe(function (response) {
                _this.selectedTrain = response;
            });
        }
    };
    CarListComponent.prototype.getFilteredCars = function (item) {
        this.selectedTrain = item;
    };
    CarListComponent.prototype.getSelectedCarType = function (item) {
        this.selectedCarType = item;
    };
    CarListComponent.prototype.getSelectedMultipleSeatsNumber = function (item) {
        this.N = item;
    };
    CarListComponent.prototype.getSelectedMultipleSeatsList = function (item) {
        this.seats = item;
    };
    CarListComponent.prototype.checkMultipleSeats = function (id) {
        this.disabled = !this.seats.includes(id);
        return { 'background-color': this.seats.includes(id) ? '#00E828' : 'red' };
    };
    CarListComponent.prototype.addSeat = function (car, seat) {
        if (this.selectedCarType != enums_1.CarType.ALL && !this.reserveSeatsIds.includes(seat.id)) {
            this.reserveSeatView.car = car.carNumber;
            this.reserveSeatView.seat = seat.number;
            this.reserveSeatsView.push(this.reserveSeatView);
            this.reserveSeatView = { car: 0, seat: 0 };
            this.reserveSeatsIds.push(seat.id);
            this.reserveButton = false;
        }
        else if (this.reserveSeatsIds.includes(seat.id)) {
            for (var i = 0; i < this.reserveSeatsIds.length; i++) {
                if (this.reserveSeatsIds[i] === seat.id) {
                    this.reserveSeatsIds.splice(i, 1);
                    this.reserveSeatsView.splice(i, 1);
                    break;
                }
            }
        }
    };
    CarListComponent.prototype.goToFinishReservation = function () {
        if (this.reserveSeatsView.length === 0) {
            this.message = 'Please select at least one seat!';
        }
        else {
            this.dataService.getSeatsIdsList(this.reserveSeatsIds);
            this.router.navigateByUrl('finish-reservation');
        }
    };
    CarListComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-car-list',
            templateUrl: './car-list.component.html'
        }),
        __metadata("design:paramtypes", [trains_service_1.TrainsService,
            trains_graphql_service_1.TrainsGraphqlService,
            feature_flag_service_1.FeatureFlagService,
            router_1.ActivatedRoute,
            router_1.Router,
            data_service_1.DataService])
    ], CarListComponent);
    return CarListComponent;
}());
exports.CarListComponent = CarListComponent;
//# sourceMappingURL=car-list.component.js.map