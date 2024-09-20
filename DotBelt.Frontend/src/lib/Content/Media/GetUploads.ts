import {
  type GraphQlQuery,
  type Maybe,
  type UploadsConnection
} from '$lib/API/GraphQL/generated'
import type {ApolloClient, NormalizedCacheObject} from "@apollo/client/core/index.js";
import {gql} from "@apollo/client/core/index.js";
import type {PaginationQuery} from "$lib/API/GraphQL/PaginationQuery";


export async function getUploads(
  client: ApolloClient<NormalizedCacheObject>,
  pagination: PaginationQuery): Promise<Maybe<UploadsConnection | undefined>> {

    const query = gql`
        query GetPosts($first: Int, $last: Int, $before: String, $after: String) {
            uploads(first: $first,
                last: $last,
                order: { publishDate: DESC },
                after: $after,
                before: $before) {
                totalCount
                pageInfo {
                    endCursor
                    hasNextPage
                    hasPreviousPage
                    startCursor
                }
                nodes {
                    id
                    title
                    publishDate
                    relativeUrl
                }
            }
        }
    `

  const {data: {uploads}, errors} = await client.query<GraphQlQuery>({
    query: query,
    variables:
      {
        last: pagination.last,
        after: pagination.after,
        before: pagination.before,
        first: pagination.first
      }
  });

  return uploads;
}
