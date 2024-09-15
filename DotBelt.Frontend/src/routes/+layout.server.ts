import {getApolloClient} from "$lib/API/GraphQL/apolloClient";
import type {LayoutServerLoad} from './$types'
import type {GraphQlQuery, SessionData} from "$lib/API/GraphQL/generated";
import {gql} from "@apollo/client/core/index.js";
import {error} from "@sveltejs/kit";
import {getApolloSSRClient} from "$lib/API/GraphQL/apolloSSRClient";

export const load: LayoutServerLoad<Promise<{ session: SessionData }>> = async ({params, fetch}) => {
  // @ts-ignore
  const client = getApolloSSRClient(fetch);

  const query = gql`
      query GetSession {
          session {
              id,
              isAuthenticated
          }
      }
  `;

  try {

    const {data: {session}, errors} = await client.query<GraphQlQuery>({
      query: query
    });

    if(errors) {
      return error(404, {
        message: 'Not found',
      });
    }

    console.log('session', session)

    return {session: session};

  } catch (e) {
    console.error("Failed to execute query: \n " + query)
    throw e;
  }
}
