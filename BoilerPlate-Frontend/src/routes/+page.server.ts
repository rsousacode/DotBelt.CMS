import type { PageServerLoad } from './$types';

type OutputType = { users: [] }

export const load: PageServerLoad<OutputType> = async ({ fetch, params, request }) => {
    
    const response = await fetch("/api/users");
    const responseData = await response.json();

    return {
        users: responseData
    }
}