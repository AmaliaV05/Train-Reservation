"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
exports.MomentUtcDateAdapter = void 0;
var core_1 = require("@angular/core");
var core_2 = require("@angular/material/core");
var material_moment_adapter_1 = require("@angular/material-moment-adapter");
var moment = require("moment");
var MomentUtcDateAdapter = /** @class */ (function (_super) {
    __extends(MomentUtcDateAdapter, _super);
    function MomentUtcDateAdapter(dateLocale) {
        return _super.call(this, dateLocale) || this;
    }
    MomentUtcDateAdapter.prototype.createDate = function (year, month, date) {
        if (month < 0 || month > 11) {
            throw Error("Invalid month index \"" + month + "\". Month index has to be between 0 and 11.");
        }
        if (date < 1) {
            throw Error("Invalid date \"" + date + "\". Date has to be greater than 0.");
        }
        var result = moment.utc({ year: year, month: month, date: date }).locale(this.locale);
        if (!result.isValid()) {
            throw Error("Invalid date \"" + date + "\" for month with index \"" + month + "\".");
        }
        return result;
    };
    MomentUtcDateAdapter = __decorate([
        (0, core_1.Injectable)(),
        __param(0, (0, core_1.Optional)()),
        __param(0, (0, core_1.Inject)(core_2.MAT_DATE_LOCALE)),
        __metadata("design:paramtypes", [String])
    ], MomentUtcDateAdapter);
    return MomentUtcDateAdapter;
}(material_moment_adapter_1.MomentDateAdapter));
exports.MomentUtcDateAdapter = MomentUtcDateAdapter;
//# sourceMappingURL=date-adapter.service.js.map