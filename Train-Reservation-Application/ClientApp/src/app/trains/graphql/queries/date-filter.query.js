"use strict";
var __makeTemplateObject = (this && this.__makeTemplateObject) || function (cooked, raw) {
    if (Object.defineProperty) { Object.defineProperty(cooked, "raw", { value: raw }); } else { cooked.raw = raw; }
    return cooked;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.GET_TRAINS_BY_DATE = void 0;
var apollo_angular_1 = require("apollo-angular");
exports.GET_TRAINS_BY_DATE = (0, apollo_angular_1.gql)(templateObject_1 || (templateObject_1 = __makeTemplateObject(["\n  query FilterTrainsByDate($filter: DateFilterInput!) {\n   trainsByDate(filter: $filter) {\n     id\n     name\n   }\n }\n"], ["\n  query FilterTrainsByDate($filter: DateFilterInput!) {\n   trainsByDate(filter: $filter) {\n     id\n     name\n   }\n }\n"])));
var templateObject_1;
//# sourceMappingURL=date-filter.query.js.map