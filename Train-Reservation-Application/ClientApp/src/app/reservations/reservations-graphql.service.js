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
exports.ReservationsGraphqlService = void 0;
var core_1 = require("@angular/core");
var apollo_angular_1 = require("apollo-angular");
var add_reservation_mutation_1 = require("./graphql/mutations/add-reservation.mutation");
var update_reservation_mutation_1 = require("./graphql/mutations/update-reservation.mutation");
var ReservationsGraphqlService = /** @class */ (function () {
    function ReservationsGraphqlService(apollo) {
        this.apollo = apollo;
    }
    ReservationsGraphqlService.prototype.makeReservation = function (socialSecurityNumber, name, email, reservationDate, reservedSeatsIds) {
        return this.apollo.mutate({
            mutation: add_reservation_mutation_1.ADD_RESERVATION,
            variables: {
                input: {
                    socialSecurityNumber: socialSecurityNumber,
                    name: name,
                    email: email,
                    reservationDate: reservationDate.toISOString(),
                    reservedSeatsIds: reservedSeatsIds
                }
            }
        });
    };
    ReservationsGraphqlService.prototype.modifyReservation = function (idReservation, id, code, reservationDate, reservedSeatsIds) {
        return this.apollo.mutate({
            mutation: update_reservation_mutation_1.UPDATE_RESERVATION,
            variables: {
                input: {
                    idReservation: idReservation,
                    reservation: {
                        id: id,
                        code: code,
                        reservationDate: reservationDate.toISOString(),
                        reservedSeatsIds: reservedSeatsIds
                    }
                }
            }
        });
    };
    ReservationsGraphqlService = __decorate([
        (0, core_1.Injectable)({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [apollo_angular_1.Apollo])
    ], ReservationsGraphqlService);
    return ReservationsGraphqlService;
}());
exports.ReservationsGraphqlService = ReservationsGraphqlService;
//# sourceMappingURL=reservations-graphql.service.js.map