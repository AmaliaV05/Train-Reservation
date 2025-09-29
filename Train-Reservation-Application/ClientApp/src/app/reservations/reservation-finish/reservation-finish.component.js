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
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var feature_flag_service_1 = require("../../core/feature-flag.service");
var feature_flags_enum_1 = require("../../core/feature-flags/feature-flags.enum");
var data_service_1 = require("../../trains/data.service");
var enums_1 = require("../../trains/enums");
var reservations_graphql_service_1 = require("../reservations-graphql.service");
var reservations_service_1 = require("../reservations.service");
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
    function ReservationFinishComponent(reservationService, reservationsGraphqlService, featureFlagService, dataService, router) {
        this.reservationService = reservationService;
        this.reservationsGraphqlService = reservationsGraphqlService;
        this.featureFlagService = featureFlagService;
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
                                type: enums_1.CarType.ALL,
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
                    type: enums_1.CarType.ALL,
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
        var socialSecurityNumber = this.reservationForm.get('socialSecurityNumber').value;
        var name = this.reservationForm.get('name').value;
        var email = this.reservationForm.get('email').value;
        this.newReservation = {
            socialSecurityNumber: socialSecurityNumber,
            name: name,
            email: email,
            reservationWithSeatsViewModel: {
                reservationDate: this.reservationDate,
                reservedSeatsIds: this.reserveSeatsIds
            }
        };
        if (this.featureFlagService.isEnabled(feature_flags_enum_1.FeatureFlags.UseGraphQL)) {
            this.reservationsGraphqlService.makeReservation(socialSecurityNumber, name, email, this.reservationDate, this.reserveSeatsIds)
                .subscribe(function (response) {
                var _a, _b;
                var result = (_a = response.data) === null || _a === void 0 ? void 0 : _a.addReservation;
                _this.ticket = result.response;
                _this.occupiedSeats = result.alternativeResponse;
                _this.message = result.message;
                _this.reservationId = (_b = _this.ticket.reservations[0]) === null || _b === void 0 ? void 0 : _b.id;
                _this.dataService.getReservationId(_this.reservationId);
            });
        }
        else {
            this.reservationService.makeReservation(this.newReservation)
                .subscribe(function (response) {
                _this.ticket = response.response;
                _this.occupiedSeats = response.alternativeResponse;
                _this.message = response.message;
                _this.reservationId = _this.ticket.reservations[0].id;
                _this.dataService.getReservationId(_this.reservationId);
            });
        }
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
            reservations_graphql_service_1.ReservationsGraphqlService,
            feature_flag_service_1.FeatureFlagService,
            data_service_1.DataService,
            router_1.Router])
    ], ReservationFinishComponent);
    return ReservationFinishComponent;
}());
exports.ReservationFinishComponent = ReservationFinishComponent;
//# sourceMappingURL=reservation-finish.component.js.map