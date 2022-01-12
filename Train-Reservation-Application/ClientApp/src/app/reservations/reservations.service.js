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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ReservationsService = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var httpOptions = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
};
var ReservationsService = /** @class */ (function () {
    function ReservationsService(httpClient, apiUrl) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
    }
    ReservationsService.prototype.post = function (path, body) {
        if (body === void 0) { body = {}; }
        return this.httpClient.post("" + this.apiUrl + path, JSON.stringify(body), httpOptions);
    };
    ReservationsService.prototype.put = function (path, body) {
        if (body === void 0) { body = {}; }
        return this.httpClient.put("" + this.apiUrl + path, JSON.stringify(body), httpOptions);
    };
    ReservationsService.prototype.makeReservation = function (newReservation) {
        return this.post("Reservations", newReservation);
    };
    ReservationsService.prototype.modifyReservation = function (reservationId, reservation) {
        return this.put("Reservations/" + reservationId, reservation);
    };
    ReservationsService = __decorate([
        (0, core_1.Injectable)({
            providedIn: 'root'
        }),
        __param(1, (0, core_1.Inject)('API_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], ReservationsService);
    return ReservationsService;
}());
exports.ReservationsService = ReservationsService;
//# sourceMappingURL=reservations.service.js.map