import { type DotBeltQuery, type Maybe, type UsersConnection } from '$lib/API/GraphQL/generated';
import type { ApolloClient, NormalizedCacheObject } from '@apollo/client/core/index.js';
import { gql } from '@apollo/client/core/index.js';
import type { PaginationQuery } from '$lib/API/GraphQL/PaginationQuery';

export async function getUsers(
  client: ApolloClient<NormalizedCacheObject>,
  pagination: PaginationQuery,
): Promise<Maybe<UsersConnection | undefined>> {
  const query = gql`
    query GetUsers($first: Int, $last: Int, $before: String, $after: String) {
      users(first: $first, last: $last, after: $after, before: $before) {
        totalCount
        pageInfo {
          endCursor
          hasNextPage
          hasPreviousPage
          startCursor
        }
        nodes {
          id
          userName
          email
          emailConfirmed
        }
      }
    }
  `;
  try {
    const {
      data: { users },
      errors,
    } = await client.query<DotBeltQuery>({
      query: query,
      variables: {
        last: pagination.last,
        after: pagination.after,
        before: pagination.before,
        first: pagination.first,
      },
    });

    return users;
  } catch (e) {
    console.error('Failed to execute query: \n ' + query);
    throw e;
  }
}
