import {
  type DotBeltMutation, type PostResponseInput,
  PostTypeEnum,
} from '$lib/API/GraphQL/generated';
import type { ApolloClient, NormalizedCacheObject } from '@apollo/client/core/index.js';
import { gql } from '@apollo/client/core/index.js';

export async function createPost(
  client: ApolloClient<NormalizedCacheObject>,
  input: PostResponseInput,
  type: PostTypeEnum,
) {
  const mutation = gql`
    mutation createPost($input: PostResponseInput!, $type: PostTypeEnum!) {
      createPost(input: { payload: $input, type: $type }) {
        postResponse {
          id
          relativeUrl
          authorId
          publishDate
          content
          description
          featuredImage {
            id
            relativeUrl
          }
        }
      }
    }
  `;

  const { data, errors } = await client.mutate<DotBeltMutation>({
    mutation: mutation,
    variables: { input: input, type: type },
  });

  if (data) {
    return data.createPost?.postResponse;
  } else {
    throw errors?.toString();
  }
}
