import {
    type DotBeltQuery,
    type Maybe, type ThumbnailsConnection
} from '$lib/API/GraphQL/generated';
import type {ApolloClient, FetchPolicy, NormalizedCacheObject} from "@apollo/client/core/index.js";
import {gql} from "@apollo/client/core/index.js";
import type {PaginationQuery} from "$lib/API/GraphQL/PaginationQuery";


export async function getUploads(
    client: ApolloClient<NormalizedCacheObject>,
    pagination: PaginationQuery, fetchPolicy: FetchPolicy = 'cache-first'): Promise<Maybe<ThumbnailsConnection | undefined>> {

    const query = gql`
        query GetUploads($first: Int, $last: Int, $before: String, $after: String) {
            thumbnails(first: $first,
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
                    relativeUrl,
                    uploadId
                }
            }
        }
    `

    const {data: {thumbnails}} = await client.query<DotBeltQuery>({
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

    return thumbnails;
}
