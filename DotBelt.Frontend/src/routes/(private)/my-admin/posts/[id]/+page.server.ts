import type { PageServerLoad } from './$types';
import type { DotBeltQuery, PostResponse, TenantResponse } from '$lib/API/GraphQL/generated';
import { getApolloSSRClient } from '$lib/API/GraphQL/apolloSSRClient';
import { gql } from '@apollo/client/core';

type ServerResponse = {
  post: PostResponse;
  tenant: TenantResponse;
}

export const load: PageServerLoad<Promise<ServerResponse>> = async ({ params, fetch }) => {
  const id = Number(params.id);

  const client = getApolloSSRClient(fetch);

  const query = gql`
    query postById($id: Int!) {

      tenantById(id: 1) {
        fullUrl
        name
      }
      postById(id: $id) {
        id
        publishDate
        content
        title
        description
        status
        relativeUrl
        featuredImageId
        featuredImage {
          id
          relativeUrl
        }
      }
    }`;

  const {
    data: { postById, tenantById }
  } = await client.query<DotBeltQuery>({
    query: query,
    variables: { id: id },
  });


  return {
    post: postById[0],
    tenant: tenantById[0]
  };
};

