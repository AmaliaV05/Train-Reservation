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
exports.FeatureFlagService = exports.getFlags = void 0;
var http_1 = require("@angular/common/http");
var core_1 = require("@angular/core");
var rxjs_1 = require("rxjs");
var operators_1 = require("rxjs/operators");
var httpOptions = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
};
function getFlags(f) {
    return function () { return f.getFlags().toPromise(); };
}
exports.getFlags = getFlags;
var FeatureFlagService = /** @class */ (function () {
    function FeatureFlagService(httpClient, apiUrl) {
        this.httpClient = httpClient;
        this.flags$ = new rxjs_1.BehaviorSubject(new Set());
        this.apiUrl = apiUrl;
    }
    FeatureFlagService.prototype.loadFlags = function (path) {
        var _this = this;
        return this.httpClient.get("" + this.apiUrl + path, httpOptions).pipe((0, operators_1.tap)(function (flagList) {
            _this.flags$.next(new Set(flagList));
        }));
    };
    FeatureFlagService.prototype.getFlags = function () {
        return this.loadFlags('FeatureFlags');
    };
    FeatureFlagService.prototype.isEnabled = function (flag) {
        return this.flags$.value.has(flag);
    };
    FeatureFlagService = __decorate([
        (0, core_1.Injectable)({
            providedIn: 'root'
        }),
        __param(1, (0, core_1.Inject)('API_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], FeatureFlagService);
    return FeatureFlagService;
}());
exports.FeatureFlagService = FeatureFlagService;
//# sourceMappingURL=feature-flag.service.js.map