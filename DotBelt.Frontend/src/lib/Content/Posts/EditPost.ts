import type {DotBeltMutation, PostResponseInput} from '$lib/API/GraphQL/generated';
import type { ApolloClient, NormalizedCacheObject } from '@apollo/client/core/index.js';
import { gql } from '@apollo/client/core/index.js';

export async function editPost(
  client: ApolloClient<NormalizedCacheObject>,
  id: number,
  input: PostResponseInput,
) {
  try {
    const mutation = gql`
      mutation editPost($input: PostResponseInput!, $postId: Int!) {
        editPost(input: { payload: $input, postId: $postId }) {
          postResponse {
            id
            relativeUrl
            content
            title
            description
            authorId
            publishDate
            modifiedDate
            featuredImage {
              id
              relativeUrl
            }
          }
        }
      }
    `;

    const { data } = await client.mutate<DotBeltMutation>({
      mutation: mutation,
      variables: { input: input, postId: id },
    });

    if (data) {
      return data.editPost?.postResponse;
    }
  } catch (e) {
    console.error(e);
    throw e;
  }
}
