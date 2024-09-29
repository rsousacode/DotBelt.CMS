import {PUBLIC_GRAPHQL_HTTP_API_URL} from '$env/static/public'
import { ApolloClient, InMemoryCache, HttpLink } from '@apollo/client/core/index.js'

type Fetch = typeof fetch;
/**
 Returns a GraphQL Client that is ready to use.
 */
export function getApolloSSRClient(fetch : Fetch | undefined = undefined) {
    const httpLink = new HttpLink({
        uri: PUBLIC_GRAPHQL_HTTP_API_URL,
        fetch: fetch
    });


    return new ApolloClient({
        ssrMode: true,
        credentials: "same-origin",
        link: httpLink,
        cache: new InMemoryCache({
            addTypename: false
        }),
    });
}