import type { PageServerLoad } from './$types';
import type {PostResponse } from '$lib/API/GraphQL/generated';

import { getPostByUrl } from '$lib/Content/Posts/GetPostByUrl';
import { error } from '@sveltejs/kit';
import { getApolloSSRClient } from '$lib/API/GraphQL/apolloSSRClient';

export const load: PageServerLoad<Promise<{ post: PostResponse }>> = async ({ params, fetch }) => {
  const url = params.url;

  const client = getApolloSSRClient(fetch);
  const post = await getPostByUrl(client, url);

  if (!post) {
    return error(404, {
      message: 'Not found',
    });
  }

  const content = post.content ? JSON.parse(post.content) : null;

  return {
    post,
    content: content
  };
};
