import type {GraphQlQuery, PostResponse} from '$lib/API/GraphQL/generated'
import type {ApolloClient, NormalizedCacheObject} from "@apollo/client/core/index.js";
import {gql} from "@apollo/client/core/index.js";

export async function getPostByUrl(client: ApolloClient<NormalizedCacheObject>, url: string): Promise<PostResponse> {

  const query = gql`
        query postByUrl($url: String!) {
            postByUrl(url: $url) {
                contentHtml,
                title,
                relativeUrl
            }
        }
    `

  const {data: {postByUrl}, errors} = await client.query<GraphQlQuery>({
    query: query,
    variables: {url: url}
  });

  return postByUrl[0];
}