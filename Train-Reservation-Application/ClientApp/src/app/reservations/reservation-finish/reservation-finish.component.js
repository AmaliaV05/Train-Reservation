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
exports.ReservationFinishComponent = void 0;
var core_1 = require("@angular/core");
var data_service_1 = require("../../home/data.service");
var reservations_service_1 = require("../reservations.service");
var ReservationFinishComponent = /** @class */ (function () {
    function ReservationFinishComponent(reservationService, dataService) {
        this.reservationService = reservationService;
        this.dataService = dataService;
    }
    ReservationFinishComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.dataService.currentMessage$.subscribe(function (message) {
            return _this.reservationDate = message;
        });
    };
    ReservationFinishComponent.prototype.ngOnDestroy = function () {
        this.subscription.unsubscribe();
    };
    ReservationFinishComponent.prototype.postReservation = function () {
        var _this = this;
        this.reservationService.post('Reservations', this.newReservation)
            .subscribe(function (response) {
            return _this.code = response;
        });
    };
    ReservationFinishComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-reservation-finish',
            templateUrl: './reservation-finish.component.html',
            styleUrls: ['./reservation-finish.component.css'],
        }),
        __metadata("design:paramtypes", [reservations_service_1.ReservationsService,
            data_service_1.DataService])
    ], ReservationFinishComponent);
    return ReservationFinishComponent;
}());
exports.ReservationFinishComponent = ReservationFinishComponent;
//# sourceMappingURL=reservation-finish.component.js.map