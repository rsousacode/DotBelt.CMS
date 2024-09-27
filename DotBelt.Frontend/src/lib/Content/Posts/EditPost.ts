import type { Edit_PostRequestInput, GraphQlMutation } from '$lib/API/GraphQL/generated';
import type { ApolloClient, NormalizedCacheObject } from '@apollo/client/core/index.js';
import { gql } from '@apollo/client/core/index.js';

export async function editPost(
  client: ApolloClient<NormalizedCacheObject>,
  id: number,
  input: Edit_PostRequestInput,
) {
  try {
    const mutation = gql`
      mutation editPost($input: Edit_PostRequestInput!, $postId: Int!) {
        editPost(input: { payload: $input, postId: $postId }) {
          editPostResult {
            success
          }
        }
      }
    `;

    const { data } = await client.mutate<GraphQlMutation>({
      mutation: mutation,
      variables: { input: input, postId: id },
    });

    if (data) {
      return data.createPost?.post;
    }
  } catch (e) {
    console.error(e);
    throw e;
  }
}
