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
exports.TrainsService = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var httpOptions = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
};
var TrainsService = /** @class */ (function () {
    function TrainsService(httpClient, apiUrl) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
    }
    TrainsService.prototype.getTrain = function (path) {
        return this.httpClient.get("" + this.apiUrl + path, httpOptions);
    };
    TrainsService.prototype.getTrains = function (path) {
        return this.httpClient.get("" + this.apiUrl + path, httpOptions);
    };
    TrainsService.prototype.getTrainsWithMultipleSeats = function (path) {
        return this.httpClient.get("" + this.apiUrl + path, httpOptions);
    };
    TrainsService.prototype.getTrainList = function (selectedDate) {
        return this.getTrains("Trains/filter-trains/" + selectedDate);
    };
    TrainsService.prototype.getTrainWithCars = function (idTrain, date, carType) {
        return this.getTrain("Trains/" + idTrain + "/" + date + "/filter-cars/" + carType);
    };
    TrainsService.prototype.getTrainWithMultipleAvailableSeats = function (idTrain, date, N) {
        return this.getTrainsWithMultipleSeats("Trains/" + idTrain + "/" + date + "/filter-cars-by-available-seats/" + N);
    };
    TrainsService = __decorate([
        (0, core_1.Injectable)({
            providedIn: 'root'
        }),
        __param(1, (0, core_1.Inject)('API_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], TrainsService);
    return TrainsService;
}());
exports.TrainsService = TrainsService;
//# sourceMappingURL=trains.service.js.map