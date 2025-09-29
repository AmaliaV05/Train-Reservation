"use strict";
var __makeTemplateObject = (this && this.__makeTemplateObject) || function (cooked, raw) {
    if (Object.defineProperty) { Object.defineProperty(cooked, "raw", { value: raw }); } else { cooked.raw = raw; }
    return cooked;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.GET_SEATS_BY_NUMBER = void 0;
var apollo_angular_1 = require("apollo-angular");
exports.GET_SEATS_BY_NUMBER = (0, apollo_angular_1.gql)(templateObject_1 || (templateObject_1 = __makeTemplateObject(["\n  query GetSeatList($filter: SeatListFilterInput!) {\n    seatList(filter: $filter)\n  }\n"], ["\n  query GetSeatList($filter: SeatListFilterInput!) {\n    seatList(filter: $filter)\n  }\n"])));
var templateObject_1;
//# sourceMappingURL=seat-list.query.js.map