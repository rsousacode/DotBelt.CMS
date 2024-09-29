import type { LayoutServerLoad } from './$types';
import type { SessionData } from '$lib/API/GraphQL/generated';
import { error } from '@sveltejs/kit';

export const load: LayoutServerLoad = async ({ parent }) => {
  const { session }: { session: SessionData } = await parent();

  if (!session) {
    return error(404, 'Unable to load session');
  }

  // Only allow authenticated users here
  // This can be changed to enforce other policies

  if (!session.isAuthenticated) {
    // returning 404 response because any user doesn't need to know
    // this route exists at all

    return error(404);
  }

  return {};
};
