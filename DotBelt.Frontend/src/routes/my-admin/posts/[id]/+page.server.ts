import {getApolloClient} from "$lib/GraphQL/apolloClient";
import type { PageServerLoad } from './$types'
import {getPostById} from "$lib/Queries/GetPostById";
import type {CreatePostInput, EditablePostInput, EditPostInput, Post} from "$lib/GraphQL/generated";
import {createPost} from "$lib/Mutations/CreatePost";
import type {Actions} from "../../../../../.svelte-kit/types/src/routes/my-admin/posts/new/$types";
import {editPost} from "$lib/Mutations/EditPost";

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
        const input: EditablePostInput = Object.fromEntries(formData.entries());

        const id = event.params.id;

        const response = await editPost(apollo, Number(id), input);

        return response;
    },
} satisfies Actions;