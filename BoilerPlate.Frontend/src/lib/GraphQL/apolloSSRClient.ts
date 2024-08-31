import {PUBLIC_GRAPHQL_HTTP_API_URL} from '$env/static/public'
import { ApolloClient, InMemoryCache, HttpLink } from '@apollo/client/core/index.js'

/**
 Returns a GraphQL Client that is ready to use.
 */
export function getApolloSSRClient() {
  const httpLink = new HttpLink({
    uri: PUBLIC_GRAPHQL_HTTP_API_URL,

  });

  return new ApolloClient({
    ssrMode: true,
    link: httpLink,
    cache: new InMemoryCache({
      addTypename: false
    }),
  });
}