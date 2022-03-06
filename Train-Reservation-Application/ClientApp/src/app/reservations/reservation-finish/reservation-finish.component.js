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
exports.ReservationFinishComponent = exports.EmailErrorStateMatcher = void 0;
var core_1 = require("@angular/core");
var reservations_service_1 = require("../reservations.service");
var data_service_1 = require("../../trains/data.service");
var train_model_1 = require("../../trains/train.model");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var EmailErrorStateMatcher = /** @class */ (function () {
    function EmailErrorStateMatcher() {
    }
    EmailErrorStateMatcher.prototype.isErrorState = function (control, form) {
        var isSubmitted = form && form.submitted;
        return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
    };
    return EmailErrorStateMatcher;
}());
exports.EmailErrorStateMatcher = EmailErrorStateMatcher;
var ReservationFinishComponent = /** @class */ (function () {
    function ReservationFinishComponent(reservationService, dataService, router) {
        this.reservationService = reservationService;
        this.dataService = dataService;
        this.router = router;
        this.newReservation = {
            socialSecurityNumber: '',
            name: '',
            email: '',
            reservationWithSeatsViewModel: {
                reservationDate: new Date,
                reservedSeatsIds: new Array()
            }
        };
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
        this.matcher = new EmailErrorStateMatcher();
        this.showTicket = false;
        this.reserveSeatsIds = new Array();
    }
    ReservationFinishComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.dataService.currentMessage$.subscribe(function (message) {
            return _this.reservationDate = message;
        });
        this.seatsListSubscription = this.dataService.currentSeatListMessage$.subscribe(function (message) {
            return _this.reserveSeatsIds = message;
        });
        this.reservationIdSubscription = this.dataService.currentModifyReservationMessage$.subscribe(function (message) {
            return _this.reservationId = message;
        });
        this.reservationForm = new forms_1.FormGroup({
            socialSecurityNumber: new forms_1.FormControl('', [forms_1.Validators.required, forms_1.Validators.minLength(13), forms_1.Validators.maxLength(13)]),
            name: new forms_1.FormControl('', [forms_1.Validators.required, forms_1.Validators.minLength(10)]),
            email: new forms_1.FormControl('', [forms_1.Validators.required, forms_1.Validators.email])
        });
    };
    ReservationFinishComponent.prototype.ngOnDestroy = function () {
        this.subscription.unsubscribe();
        this.seatsListSubscription.unsubscribe();
        this.reservationIdSubscription.unsubscribe();
    };
    ReservationFinishComponent.prototype.postReservation = function () {
        var _this = this;
        this.newReservation = {
            socialSecurityNumber: this.reservationForm.get('socialSecurityNumber').value,
            name: this.reservationForm.get('name').value,
            email: this.reservationForm.get('email').value,
            reservationWithSeatsViewModel: {
                reservationDate: this.reservationDate,
                reservedSeatsIds: this.reserveSeatsIds
            }
        };
        this.reservationService.makeReservation(this.newReservation)
            .subscribe(function (response) {
            _this.ticket = response.response;
            _this.occupiedSeats = response.alternativeResponse;
            _this.message = response.message;
            _this.reservationId = _this.ticket.reservations[0].id;
            _this.dataService.getReservationId(_this.reservationId);
        });
        this.reservationForm.reset();
        this.showTicket = true;
    };
    ReservationFinishComponent.prototype.goToModifyReservation = function () {
        this.router.navigateByUrl('modify-reservation');
    };
    ReservationFinishComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-reservation-finish',
            templateUrl: './reservation-finish.component.html',
            styleUrls: ['./reservation-finish.component.css'],
        }),
        __metadata("design:paramtypes", [reservations_service_1.ReservationsService,
            data_service_1.DataService,
            router_1.Router])
    ], ReservationFinishComponent);
    return ReservationFinishComponent;
}());
exports.ReservationFinishComponent = ReservationFinishComponent;
//# sourceMappingURL=reservation-finish.component.js.map