import {
  type GraphQlQuery,
  type Maybe,
  type SessionData,
} from '$lib/API/GraphQL/generated';
import { gql } from '@apollo/client/core/index.js';
import { getApolloSSRClient } from '$lib/API/GraphQL/apolloSSRClient';

export async function getSessionSSR(
  fetch: any,
): Promise<Maybe<SessionData | undefined>> {
  // @ts-ignore
  const client = getApolloSSRClient(fetch);

  const query = gql`
    query GetSession {
      session {
        id
        email
        userName
        isAuthenticated
      }
    }
  `;

  try {
    const {
      data: { session },
      errors,
    } = await client.query<GraphQlQuery>({
      query: query,
    });

    return session;
  } catch (e) {
    console.error('Failed to execute query: \n ' + query.definitions);
    throw e;
  }
}
