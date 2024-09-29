import { createPost } from '$lib/Content/Posts/CreatePost';
import { type Create_PostRequestInput, PostTypeEnum } from '$lib/API/GraphQL/generated';
import type { Actions } from '@sveltejs/kit';
import { getApolloSSRClient } from '$lib/API/GraphQL/apolloSSRClient';

export const actions = {
  default: async ({ fetch, request, url }) => {
    const apollo = getApolloSSRClient(fetch);

    const postType = url.searchParams.get('type');

    const formData = await request.formData();
    const input: Create_PostRequestInput = Object.fromEntries(
      formData.entries(),
    ) as Create_PostRequestInput;

    if (postType !== null) {
      return await createPost(apollo, input, postType.toUpperCase() as PostTypeEnum);
    }
  },
} satisfies Actions;
