import { NgModule } from '@angular/core';
import { graphqlProvider } from './config/graphql.config';
import { apiProvider } from './config/api.config';
import { baseUrlProvider } from './config/baseUrl.config';

@NgModule({
  providers: [graphqlProvider, apiProvider, baseUrlProvider],
})
export class CoreModule { }
