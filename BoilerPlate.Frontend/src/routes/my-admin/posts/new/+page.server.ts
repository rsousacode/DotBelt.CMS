import type { Actions } from './$types';
import { getApolloClient } from "$lib/GraphQL/apolloClient";
import { createPost } from "$lib/Mutations/CreatePost";
import type { CreatePostInput } from "$lib/GraphQL/generated";

export const actions = {
    default: async (event) => {
        const apollo = getApolloClient();

        const formData = await event.request.formData();
        const input: CreatePostInput = Object.fromEntries(formData.entries());

        const response = await createPost(apollo, input);

        return response;
    },
} satisfies Actions;