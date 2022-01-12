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
var data_service_1 = require("../data.service");
var train_model_1 = require("../train.model");
var trains_service_1 = require("../trains.service");
var SelectedSeatsView = /** @class */ (function () {
    function SelectedSeatsView() {
    }
    return SelectedSeatsView;
}());
var CarListComponent = /** @class */ (function () {
    function CarListComponent(trainService, route, router, dataService) {
        this.trainService = trainService;
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
                    type: train_model_1.CarType.All,
                    seats: [{
                            id: 0,
                            number: 0,
                            seatCalendars: [{
                                    seatAvailability: false
                                }]
                        }]
                }]
        };
        this.selectedCarType = train_model_1.CarType.All;
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
        this.idTrain = this.route.snapshot.params['id'];
        this.getSelectedTrain();
    };
    CarListComponent.prototype.ngOnDestroy = function () {
        this.subscription.unsubscribe();
        this.seatsListSubscription.unsubscribe();
    };
    CarListComponent.prototype.getSelectedTrain = function () {
        var _this = this;
        this.trainService.getTrainWithCars(this.idTrain, this.selectedDate, this.selectedCarType)
            .subscribe(function (response) {
            _this.selectedTrain = response;
        });
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
        if (this.selectedCarType != train_model_1.CarType.All && !this.reserveSeatsIds.includes(seat.id)) {
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
            router_1.ActivatedRoute,
            router_1.Router,
            data_service_1.DataService])
    ], CarListComponent);
    return CarListComponent;
}());
exports.CarListComponent = CarListComponent;
//# sourceMappingURL=car-list.component.js.map