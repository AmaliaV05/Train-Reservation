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
exports.TrainsGraphqlService = void 0;
var core_1 = require("@angular/core");
var apollo_angular_1 = require("apollo-angular");
var enums_1 = require("./enums");
var car_list_query_1 = require("./graphql/queries/car-list.query");
var date_filter_query_1 = require("./graphql/queries/date-filter.query");
var seat_list_query_1 = require("./graphql/queries/seat-list.query");
var TrainsGraphqlService = /** @class */ (function () {
    function TrainsGraphqlService(apollo) {
        this.apollo = apollo;
    }
    TrainsGraphqlService.prototype.getTrainsByDate = function (selectedDay) {
        return this.apollo.query({
            query: date_filter_query_1.GET_TRAINS_BY_DATE,
            variables: {
                filter: {
                    dayOfWeek: enums_1.DayOfWeek[selectedDay].toString()
                }
            }
        });
    };
    TrainsGraphqlService.prototype.getCarsByType = function (trainId, selectedDate, selectedCarType) {
        return this.apollo.query({
            query: car_list_query_1.GET_CARS_BY_TYPE,
            variables: {
                filter: {
                    id: trainId,
                    calendarDate: selectedDate.toISOString(),
                    type: enums_1.CarType[selectedCarType]
                }
            }
        });
    };
    TrainsGraphqlService.prototype.getSeatList = function (trainId, selectedDate, numberOfSeats) {
        return this.apollo.query({
            query: seat_list_query_1.GET_SEATS_BY_NUMBER,
            variables: {
                filter: {
                    id: trainId,
                    calendarDate: selectedDate.toISOString(),
                    n: numberOfSeats
                }
            }
        });
    };
    TrainsGraphqlService = __decorate([
        (0, core_1.Injectable)({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [apollo_angular_1.Apollo])
    ], TrainsGraphqlService);
    return TrainsGraphqlService;
}());
exports.TrainsGraphqlService = TrainsGraphqlService;
//# sourceMappingURL=trains-graphql.service.js.map