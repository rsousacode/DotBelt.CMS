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
      tenantById(id: 1) {
        fullUrl
        name
      }
    }`;

  const {
    data: { tenantById }
  } = await client.query<DotBeltQuery>({
    query: query
  });


  return {
    tenant: tenantById[0]
  };
};

