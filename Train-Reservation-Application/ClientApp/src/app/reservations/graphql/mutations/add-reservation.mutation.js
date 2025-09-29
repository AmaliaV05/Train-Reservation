"use strict";
var __makeTemplateObject = (this && this.__makeTemplateObject) || function (cooked, raw) {
    if (Object.defineProperty) { Object.defineProperty(cooked, "raw", { value: raw }); } else { cooked.raw = raw; }
    return cooked;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ADD_RESERVATION = void 0;
var apollo_angular_1 = require("apollo-angular");
exports.ADD_RESERVATION = (0, apollo_angular_1.gql)(templateObject_1 || (templateObject_1 = __makeTemplateObject(["\nmutation NewReservation($input: AddReservationInput!) {\n  addReservation(input: $input) {\n    message\n    response {\n      name\n      reservations {\n        id\n        code\n        reservationDate\n        seats {\n          id\n          number\n          car {\n            id\n            carNumber\n            type\n            train {\n              id\n              name\n            }\n          }\n        }\n      }\n    }\n    alternativeResponse {\n      id\n      number\n      car {\n        id\n        carNumber\n        type\n        train {\n          id\n          name\n        }\n      }\n    }\n  }\n}\n"], ["\nmutation NewReservation($input: AddReservationInput!) {\n  addReservation(input: $input) {\n    message\n    response {\n      name\n      reservations {\n        id\n        code\n        reservationDate\n        seats {\n          id\n          number\n          car {\n            id\n            carNumber\n            type\n            train {\n              id\n              name\n            }\n          }\n        }\n      }\n    }\n    alternativeResponse {\n      id\n      number\n      car {\n        id\n        carNumber\n        type\n        train {\n          id\n          name\n        }\n      }\n    }\n  }\n}\n"])));
var templateObject_1;
//# sourceMappingURL=add-reservation.mutation.js.map