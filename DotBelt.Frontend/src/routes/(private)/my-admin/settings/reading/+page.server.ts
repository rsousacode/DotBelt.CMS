import type { PageServerLoad } from './$types';
import type { DotBeltQuery, TenantResponse } from '$lib/API/GraphQL/generated';
import { getApolloSSRClient } from '$lib/API/GraphQL/apolloSSRClient';
import { gql } from '@apollo/client/core';

type ServerResponse = {
  tenant: TenantResponse;
}

export const load: PageServerLoad<Promise<ServerResponse>> = async ({ fetch }) => {

  const client = getApolloSSRClient(fetch);

  const query = gql`
    query getTenant {
      publishedPages {
        id
        title
      }
      
      tenantById(id: 1) {
        id
        allowedFileTypes
        fullUrl
        name
        homepageId
      }
    }`;

  const {
    data: { tenantById, publishedPages }
  } = await client.query<DotBeltQuery>({
    query: query
  });


  return {
    posts: publishedPages,
    tenant: tenantById[0]
  };
};

