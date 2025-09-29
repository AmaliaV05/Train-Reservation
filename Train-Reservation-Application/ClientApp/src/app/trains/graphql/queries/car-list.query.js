"use strict";
var __makeTemplateObject = (this && this.__makeTemplateObject) || function (cooked, raw) {
    if (Object.defineProperty) { Object.defineProperty(cooked, "raw", { value: raw }); } else { cooked.raw = raw; }
    return cooked;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.GET_CARS_BY_TYPE = void 0;
var apollo_angular_1 = require("apollo-angular");
exports.GET_CARS_BY_TYPE = (0, apollo_angular_1.gql)(templateObject_1 || (templateObject_1 = __makeTemplateObject(["\n  query FilterCarsByType($filter: CarTypeFilterInput!) {\n    carsByType(filter: $filter) {\n      id\n      name\n      cars {\n        carNumber\n        type\n        seats {\n          id\n          number\n          seatCalendars {\n            seatAvailability\n          }\n        }\n      }\n    }\n  }\n"], ["\n  query FilterCarsByType($filter: CarTypeFilterInput!) {\n    carsByType(filter: $filter) {\n      id\n      name\n      cars {\n        carNumber\n        type\n        seats {\n          id\n          number\n          seatCalendars {\n            seatAvailability\n          }\n        }\n      }\n    }\n  }\n"])));
var templateObject_1;
//# sourceMappingURL=car-list.query.js.map