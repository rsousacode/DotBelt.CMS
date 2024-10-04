import type { PageServerLoad } from './$types';
import type {DotBeltQuery, PostResponse} from '$lib/API/GraphQL/generated';

import { getApolloSSRClient } from '$lib/API/GraphQL/apolloSSRClient';
import {gql} from "@apollo/client/core";

export const load: PageServerLoad<Promise<{ post: PostResponse }>> = async ({ fetch }) => {

  const client = getApolloSSRClient(fetch);
  const query = gql`
    query homepage {
      homepage {
        content
        title
        relativeUrl
      }
    }
  `;

  const {
    data: { homepage }
  } = await client.query<DotBeltQuery>({
    query: query
  });

  const post = homepage[0];

  const content = post.content ? JSON.parse(post.content) : null;

  return {
    post,
    content: content
  };
};
