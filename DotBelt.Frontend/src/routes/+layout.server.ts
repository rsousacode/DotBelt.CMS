import type { LayoutServerLoad } from './$types';
import type { SessionData } from '$lib/API/GraphQL/generated';
import { error } from '@sveltejs/kit';
import { getSessionSSR } from '$lib/Users/GetSessionSSR';

export const load: LayoutServerLoad<
  Promise<{ session: SessionData }>
> = async ({ fetch }) => {
  const sessionData = await getSessionSSR(fetch);

  if (!sessionData) {
    return error(404, 'Unable to load session');
  }

  return { session: sessionData };
};
