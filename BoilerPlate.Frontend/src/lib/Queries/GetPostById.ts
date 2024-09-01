import type {GraphQlQuery, Post} from '$lib/GraphQL/generated'
import type {ApolloClient, NormalizedCacheObject} from "@apollo/client/core/index.js";
import {gql} from "@apollo/client/core/index.js";

export async function getPostById(client : ApolloClient<NormalizedCacheObject>, id: number) : Promise<Post> {

    const query = gql`
        query postById($id: Int!) {
            postById(id: $id) {
                content,
                title,
                urlName
            }
        }
    `

    const { data: { postById }, errors } = await client.query<GraphQlQuery>({
        query: query,
        variables: {id: id}
    });

    return postById[0];
}