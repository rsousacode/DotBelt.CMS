import {getApolloClient} from "$lib/API/GraphQL/apolloClient";
import type { PageServerLoad } from './$types'
import type { PostResponse } from "$lib/API/GraphQL/generated";

import {getPostByUrl} from "$lib/Content/Posts/GetPostByUrl";
import {error} from "@sveltejs/kit";

export const load: PageServerLoad<Promise<{post: PostResponse}>> = async ({ params }) => {

    const url = params.url;

    const client = getApolloClient();
    const post = await getPostByUrl(client, url);

    if(!post) {
        return error(404, {
            message: 'Not found',
        });
    }


    return {
        post
    }
}
