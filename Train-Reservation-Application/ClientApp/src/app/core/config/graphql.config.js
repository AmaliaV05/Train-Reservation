"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.graphqlProvider = exports.getGraphqlUrl = void 0;
var apollo_angular_1 = require("apollo-angular");
var http_1 = require("apollo-angular/http");
var core_1 = require("@apollo/client/core");
function getGraphqlUrl(httpLink) {
    return {
        link: httpLink.create({ uri: '/graphql', }),
        cache: new core_1.InMemoryCache()
    };
}
exports.getGraphqlUrl = getGraphqlUrl;
exports.graphqlProvider = {
    provide: apollo_angular_1.APOLLO_OPTIONS,
    useFactory: getGraphqlUrl,
    deps: [http_1.HttpLink]
};
//# sourceMappingURL=graphql.config.js.map