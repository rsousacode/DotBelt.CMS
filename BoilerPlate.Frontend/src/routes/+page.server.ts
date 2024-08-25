import type { PageServerLoad } from './$types';
import { PUBLIC_BASE_URL } from '$env/static/public'

type OutputType = { users: [] }

export const load: PageServerLoad<OutputType> = async ({ fetch, params, request }) => {
    
    const response = await fetch(PUBLIC_BASE_URL + "/api/users");
    const responseData = await response.json();

    return {
        users: responseData
    }
}