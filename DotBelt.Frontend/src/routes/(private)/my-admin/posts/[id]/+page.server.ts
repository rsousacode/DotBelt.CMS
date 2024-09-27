import type { Actions, PageServerLoad } from './$types';
import { getPostById } from '$lib/Content/Posts/GetPostById';
import type {
  Edit_PostRequestInput,
  PostResponse,
} from '$lib/API/GraphQL/generated';
import { editPost } from '$lib/Content/Posts/EditPost';
import { getApolloSSRClient } from '$lib/API/GraphQL/apolloSSRClient';

export const load: PageServerLoad<Promise<{ post: PostResponse }>> = async ({
  params,
  fetch,
}) => {
  const id = params.id;

  const client = getApolloSSRClient(fetch);
  const post = await getPostById(client, Number(id));

  return {
    post,
  };
};

export const actions = {
  default: async ({ request, params, fetch }) => {
    const apollo = getApolloSSRClient(fetch);

    const formData = await request.formData();
    const input: Edit_PostRequestInput = Object.fromEntries(
      formData.entries(),
    ) as Edit_PostRequestInput;

    const id = params.id;

    return await editPost(apollo, Number(id), input);
  },
} satisfies Actions;
