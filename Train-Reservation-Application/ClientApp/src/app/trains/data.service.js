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
exports.DataService = void 0;
var core_1 = require("@angular/core");
var rxjs_1 = require("rxjs");
var DataService = /** @class */ (function () {
    function DataService() {
        this.messageSource = new rxjs_1.BehaviorSubject(new Date);
        this.currentMessage$ = this.messageSource.asObservable();
        this.seatListSource = new rxjs_1.BehaviorSubject(new Array());
        this.currentSeatListMessage$ = this.seatListSource.asObservable();
        this.modifyReservationIdSource = new rxjs_1.BehaviorSubject(0);
        this.currentModifyReservationMessage$ = this.modifyReservationIdSource.asObservable();
    }
    DataService.prototype.getReservationDate = function (date) {
        this.messageSource.next(date);
    };
    DataService.prototype.getSeatsIdsList = function (list) {
        this.seatListSource.next(list);
    };
    DataService.prototype.getReservationId = function (id) {
        this.modifyReservationIdSource.next(id);
    };
    DataService = __decorate([
        (0, core_1.Injectable)({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [])
    ], DataService);
    return DataService;
}());
exports.DataService = DataService;
//# sourceMappingURL=data.service.js.map