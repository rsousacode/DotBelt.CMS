import {PUBLIC_GRAPHQL_HTTP_API_URL} from '$env/static/public'
import {ApolloClient, HttpLink, InMemoryCache} from "@apollo/client/core/index.js";

/**
 Returns a GraphQL Client that is ready to use.
 */
export function getApolloClient() {
  const httpLink = new HttpLink({
    uri: PUBLIC_GRAPHQL_HTTP_API_URL,

  });

  return new ApolloClient({
    link: httpLink,
    cache: new InMemoryCache({
      /* Workaround to deal with query errors that can appear */
      addTypename: false
    }),
  });
}