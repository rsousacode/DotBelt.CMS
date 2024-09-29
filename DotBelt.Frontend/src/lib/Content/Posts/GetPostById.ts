import type { DotBeltQuery, PostResponse } from '$lib/API/GraphQL/generated';
import type { ApolloClient, NormalizedCacheObject } from '@apollo/client/core/index.js';
import { gql } from '@apollo/client/core/index.js';

export async function getPostById(
  client: ApolloClient<NormalizedCacheObject>,
  id: number,
): Promise<PostResponse> {
  const query = gql`
    query postById($id: Int!) {
      postById(id: $id) {
        content
        title
        description
        relativeUrl
      }
    }
  `;

  const {
    data: { postById },
    errors,
  } = await client.query<DotBeltQuery>({
    query: query,
    variables: { id: id },
  });

  return postById[0];
}
