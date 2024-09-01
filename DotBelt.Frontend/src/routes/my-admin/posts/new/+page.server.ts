import type { Actions } from './$types';
import { getApolloClient } from "$lib/GraphQL/apolloClient";
import { createPost } from "$lib/Mutations/CreatePost";
import type {CreatePostInput, EditablePostInput} from "$lib/GraphQL/generated";

export const actions = {
    default: async (event) => {
        const apollo = getApolloClient();

        const postType = event.url.searchParams.get("type");

        const formData = await event.request.formData();
        const input: EditablePostInput = Object.fromEntries(formData.entries());

        const response = await createPost(apollo, input, postType.toUpperCase());

        return response;
    },
} satisfies Actions;