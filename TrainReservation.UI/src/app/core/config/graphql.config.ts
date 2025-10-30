import { APOLLO_OPTIONS } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { InMemoryCache } from '@apollo/client/core';

export function getGraphqlUrl(httpLink: HttpLink) {
  return {
    link: httpLink.create({ uri: '/graphql', }),
    cache: new InMemoryCache()
  }
}

export const graphqlProvider = {
  provide: APOLLO_OPTIONS,
  useFactory: getGraphqlUrl,
  deps: [HttpLink]
};
