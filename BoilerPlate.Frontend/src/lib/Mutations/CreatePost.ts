import type {CreatePostInput, GraphQlMutation} from '$lib/GraphQL/generated'
import type {ApolloClient, NormalizedCacheObject} from "@apollo/client/core/index.js";
import {gql} from "@apollo/client/core/index.js";

export async function createPost(client : ApolloClient<NormalizedCacheObject>, input: CreatePostInput) {

    const mutation = gql`
        mutation createPost($input: CreatePostInput!) {
            createPost(payload: $input) {
                post {
                    id
                    urlName
                    content
                    description
                }
            }
        }
    `

    const { data, errors } = await client.mutate<GraphQlMutation>({
        mutation: mutation,
        variables: {input: input}
    });

    if(data) {
        return data.createPost?.post;

    }

    return 500;
}