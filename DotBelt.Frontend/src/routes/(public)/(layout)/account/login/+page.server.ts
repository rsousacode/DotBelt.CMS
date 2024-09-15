import type { PageServerLoad } from './$types'
import type {SessionData} from "$lib/API/GraphQL/generated";
import {error} from "@sveltejs/kit";

export const load: PageServerLoad = async ({parent}) => {

  // Access the parent layout data
  const {session} : {session: SessionData} = await parent();

  if(session.isAuthenticated) {
    return error(404, {
      message: 'Not found',
    });
  }

  return {}
}