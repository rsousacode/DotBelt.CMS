import {getApolloClient} from "$lib/GraphQL/apolloClient";
import type { PageServerLoad } from './$types'
import {getPostById} from "$lib/Queries/GetPostById";
import type {Post} from "$lib/GraphQL/generated";

export const load: PageServerLoad<Promise<{post: Post}>> = async ({ params }) => {

    const id = params.id;

    const client = getApolloClient();
    const post = await getPostById(client, Number(id));


    return {
        post
    }
}