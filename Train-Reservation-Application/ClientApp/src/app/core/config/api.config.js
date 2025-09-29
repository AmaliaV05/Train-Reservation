"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.apiProvider = exports.getApiUrl = void 0;
function getApiUrl() {
    return '/api/';
}
exports.getApiUrl = getApiUrl;
exports.apiProvider = {
    provide: 'API_URL',
    useFactory: getApiUrl,
    deps: [],
};
//# sourceMappingURL=api.config.js.map