import {getApolloClient} from "$lib/API/GraphQL/apolloClient";
import type { PageServerLoad } from './$types'
import {getPostById} from "$lib/Content/Posts/GetPostById";
import type {  Edit_PostRequestInput, Post} from "$lib/API/GraphQL/generated";
import type {Actions} from "./$types";
import {editPost} from "$lib/Content/Posts/EditPost";

export const load: PageServerLoad<Promise<{post: Post}>> = async ({ params }) => {

    const id = params.id;

    const client = getApolloClient();
    const post = await getPostById(client, Number(id));


    return {
        post
    }
}

export const actions = {
    default: async (event) => {
        const apollo = getApolloClient();

        const formData = await event.request.formData();
        const input: Edit_PostRequestInput = Object.fromEntries(formData.entries()) as Edit_PostRequestInput;

        const id = event.params.id;

        const response = await editPost(apollo, Number(id), input);

        return response;
    },
} satisfies Actions;