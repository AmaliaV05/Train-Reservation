"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.baseUrlProvider = exports.getBaseUrl = void 0;
function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
exports.getBaseUrl = getBaseUrl;
exports.baseUrlProvider = {
    provide: 'BASE_URL',
    useFactory: getBaseUrl,
    deps: [],
};
//# sourceMappingURL=baseUrl.config.js.map