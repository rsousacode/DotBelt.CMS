import {
  type GraphQlQuery,
  type Maybe,
  type UploadsConnection
} from '$lib/API/GraphQL/generated'
import type {ApolloClient, FetchPolicy, NormalizedCacheObject} from "@apollo/client/core/index.js";
import {gql} from "@apollo/client/core/index.js";
import type {PaginationQuery} from "$lib/API/GraphQL/PaginationQuery";


export async function getUploads(
  client: ApolloClient<NormalizedCacheObject>,
  pagination: PaginationQuery, fetchPolicy: FetchPolicy = 'cache-first'): Promise<Maybe<UploadsConnection | undefined>> {

    const query = gql`
        query GetUploads($first: Int, $last: Int, $before: String, $after: String) {
            uploads(first: $first,
                last: $last,
                order: { publishDate: DESC },
                after: $after,
                before: $before) {
                pageInfo {
                    endCursor
                    hasNextPage
                    hasPreviousPage
                    startCursor
                }
                nodes {
                    id
                    publishDate
                    relativeUrl
                }
            }
        }
    `

  const {data: {uploads}} = await client.query<GraphQlQuery>({
    query: query,
    variables:
      {
        last: pagination.last,
        after: pagination.after,
        before: pagination.before,
        first: pagination.first
      },
    fetchPolicy: fetchPolicy,
  });

  return uploads;
}
