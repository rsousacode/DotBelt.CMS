import {getApolloClient} from "$lib/GraphQL/apolloClient";
import type { PageServerLoad } from '../../../../.svelte-kit/types/src/routes'
import type { Post } from "$lib/GraphQL/generated";

import {getPostByUrl} from "$lib/Queries/GetPostByUrl";
import {error} from "@sveltejs/kit";

export const load: PageServerLoad<Promise<{post: Post}>> = async ({ params }) => {

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
