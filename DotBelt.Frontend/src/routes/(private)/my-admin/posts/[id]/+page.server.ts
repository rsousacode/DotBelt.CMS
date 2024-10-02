import type { Actions, PageServerLoad } from './$types';
import { getPostById } from '$lib/Content/Posts/GetPostById';
import type {PostResponse, PostResponseInput} from '$lib/API/GraphQL/generated';
import { getApolloSSRClient } from '$lib/API/GraphQL/apolloSSRClient';

export const load: PageServerLoad<Promise<{ post: PostResponse }>> = async ({ params, fetch }) => {
  const id = params.id;

  const client = getApolloSSRClient(fetch);
  const post = await getPostById(client, Number(id));

  return {
    post,
  };
};

