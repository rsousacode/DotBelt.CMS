import {
  type DotBeltQuery,
  type Maybe,
  type PostsConnection,
  PostTypeEnum,
} from '$lib/API/GraphQL/generated';
import type { ApolloClient, FetchPolicy, NormalizedCacheObject } from '@apollo/client/core/index.js';
import { gql } from '@apollo/client/core/index.js';
import type { PaginationQuery } from '$lib/API/GraphQL/PaginationQuery';

export async function getPosts(
  client: ApolloClient<NormalizedCacheObject>,
  type: PostTypeEnum,
  pagination: PaginationQuery,
  fetchPolicy: FetchPolicy
): Promise<Maybe<PostsConnection | undefined>> {
  const query = gql`
    query GetPosts($type: PostTypeEnum!, $first: Int, $last: Int, $before: String, $after: String) {
      posts(
        first: $first
        last: $last
        order: { publishDate: DESC }
        after: $after
        before: $before
        where: { postType: { eq: $type } }
      ) {
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
          status
          relativeUrl
          author {
            userName
          }
        }
      }
    }
  `;

  const {
    data: { posts }
  } = await client.query<DotBeltQuery>({
    query: query,
    fetchPolicy: fetchPolicy,
    variables: {
      type: type,
      last: pagination.last,
      after: pagination.after,
      before: pagination.before,
      first: pagination.first,
    },
  });

  return posts;
}
