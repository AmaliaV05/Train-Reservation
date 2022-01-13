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
exports.ModifyReservationComponent = void 0;
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var data_service_1 = require("../../trains/data.service");
var train_model_1 = require("../../trains/train.model");
var reservations_service_1 = require("../reservations.service");
var ModifyReservationComponent = /** @class */ (function () {
    function ModifyReservationComponent(dataService, reservationsService) {
        this.dataService = dataService;
        this.reservationsService = reservationsService;
        this.modifiedReservation = {
            id: 0,
            code: '',
            reservationDate: new Date,
            reservedSeatsIds: new Array()
        };
        this.code = new forms_1.FormControl('', [forms_1.Validators.required, forms_1.Validators.minLength(7), forms_1.Validators.maxLength(7)]);
        this.reserveSeatsIds = new Array();
        this.ticket = {
            name: '',
            reservations: [{
                    id: 0,
                    code: '',
                    reservationDate: new Date,
                    seats: [{
                            id: 0,
                            number: 0,
                            car: {
                                id: 0,
                                carNumber: 0,
                                type: train_model_1.CarType.All,
                                train: {
                                    id: 0,
                                    name: ''
                                }
                            }
                        }]
                }]
        };
        this.occupiedSeats = [{
                id: 0,
                number: 0,
                car: {
                    id: 0,
                    carNumber: 0,
                    type: train_model_1.CarType.All,
                    train: {
                        id: 0,
                        name: ''
                    }
                }
            }];
        this.showTicket = false;
    }
    ModifyReservationComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.dataService.currentMessage$.subscribe(function (message) {
            return _this.selectedDate = message;
        });
        this.seatsListSubscription = this.dataService.currentSeatListMessage$.subscribe(function (message) {
            return _this.reserveSeatsIds = message;
        });
        this.reservationIdSubscription = this.dataService.currentModifyReservationMessage$.subscribe(function (message) {
            return _this.reservationId = message;
        });
    };
    ModifyReservationComponent.prototype.ngOnDestroy = function () {
        this.subscription.unsubscribe();
        this.seatsListSubscription.unsubscribe();
    };
    ModifyReservationComponent.prototype.modifyReservation = function () {
        var _this = this;
        this.modifiedReservation = {
            id: this.reservationId,
            code: this.code.value,
            reservationDate: this.selectedDate,
            reservedSeatsIds: this.reserveSeatsIds
        };
        this.reservationsService.modifyReservation(this.reservationId, this.modifiedReservation)
            .subscribe(function (response) {
            _this.ticket = response.response;
            _this.occupiedSeats = response.alternativeResponse;
            _this.message = response.message;
        });
        this.reserveSeatsIds = [];
        this.modifyReservationForm.resetForm();
        this.showTicket = true;
    };
    __decorate([
        (0, core_1.ViewChild)("modifyReservationForm"),
        __metadata("design:type", forms_1.NgForm)
    ], ModifyReservationComponent.prototype, "modifyReservationForm", void 0);
    ModifyReservationComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-modify-reservation',
            templateUrl: './modify-reservation.component.html'
        }),
        __metadata("design:paramtypes", [data_service_1.DataService,
            reservations_service_1.ReservationsService])
    ], ModifyReservationComponent);
    return ModifyReservationComponent;
}());
exports.ModifyReservationComponent = ModifyReservationComponent;
//# sourceMappingURL=modify-reservation.component.js.map