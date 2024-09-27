import {
  type GraphQlQuery,
  type Maybe,
  type PostsConnection,
  PostTypeEnum,
} from '$lib/API/GraphQL/generated';
import type {
  ApolloClient,
  NormalizedCacheObject,
} from '@apollo/client/core/index.js';
import { gql } from '@apollo/client/core/index.js';
import type { PaginationQuery } from '$lib/API/GraphQL/PaginationQuery';

export async function getPosts(
  client: ApolloClient<NormalizedCacheObject>,
  type: PostTypeEnum,
  pagination: PaginationQuery,
): Promise<Maybe<PostsConnection | undefined>> {
  const query = gql`
    query GetPosts(
      $type: PostTypeEnum!
      $first: Int
      $last: Int
      $before: String
      $after: String
    ) {
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
          relativeUrl
          author {
            userName
          }
        }
      }
    }
  `;

  const {
    data: { posts },
    errors,
  } = await client.query<GraphQlQuery>({
    query: query,
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
