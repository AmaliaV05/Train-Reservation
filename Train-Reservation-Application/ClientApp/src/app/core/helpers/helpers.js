"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getISOStringWithoutTimezone = void 0;
function getISOStringWithoutTimezone(date) {
    return date.toISOString().split('T')[0];
}
exports.getISOStringWithoutTimezone = getISOStringWithoutTimezone;
//# sourceMappingURL=helpers.js.map