import {getApolloClient} from "$lib/GraphQL/apolloClient";
import type { PageServerLoad } from './$types'
import type { Post } from "$lib/GraphQL/generated";

import {getPostByUrl} from "$lib/Queries/GetPostByUrl";

export const load: PageServerLoad<Promise<{post: Post}>> = async ({ params }) => {

    const url = params.url;

    const client = getApolloClient();
    const post = await getPostByUrl(client, url);


    return {
        post
    }
}
