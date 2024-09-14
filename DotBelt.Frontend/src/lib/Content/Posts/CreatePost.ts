import {type Edit_PostRequestInput, type GraphQlMutation, PostTypeEnum} from '$lib/API/GraphQL/generated'
import type {ApolloClient, NormalizedCacheObject} from "@apollo/client/core/index.js";
import {gql} from "@apollo/client/core/index.js";

export async function createPost(client: ApolloClient<NormalizedCacheObject>, input: Edit_PostRequestInput, type: PostTypeEnum) {

  const mutation = gql`
        mutation createPost($input: Create_PostRequestInput!, $type: PostTypeEnum!) {
            createPost(input: {payload: $input, type: $type}) {
                post {
                    id
                    urlName
                    content
                    description
                }
            }
        }
    `

  const {data, errors} = await client.mutate<GraphQlMutation>({
    mutation: mutation,
    variables: {input: input, type: type}
  });

  if (data) {
    return data.createPost?.post;

  } else {
    throw "Error processing request";
  }
}