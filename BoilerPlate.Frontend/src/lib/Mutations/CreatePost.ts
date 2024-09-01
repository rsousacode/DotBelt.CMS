import type {EditablePostInput, GraphQlMutation} from '$lib/GraphQL/generated'
import type {ApolloClient, NormalizedCacheObject} from "@apollo/client/core/index.js";
import {gql} from "@apollo/client/core/index.js";

export async function createPost(client : ApolloClient<NormalizedCacheObject>, input: EditablePostInput) {

    const mutation = gql`
        mutation createPost($input: EditablePostInput!) {
            createPost(input: {payload: $input}) {
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

    } else {
        throw "Error processing request";
    }
}