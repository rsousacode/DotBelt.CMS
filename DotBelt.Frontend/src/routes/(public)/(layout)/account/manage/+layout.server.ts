import type {LayoutServerLoad} from './$types'
import type { SessionData} from "$lib/API/GraphQL/generated";
import {error} from "@sveltejs/kit";

export const load: LayoutServerLoad = async ({parent}) => {
  const {session} : {session: SessionData} = await parent();

  if(!session) {
    return error(404, "Unable to load session");
  }

  if (!session.isAuthenticated) {
    return error(404);
  }

  return {};
}
