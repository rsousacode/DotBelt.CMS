import {getApolloClient} from "$lib/API/GraphQL/apolloClient";
import {createPost} from "$lib/Content/Posts/CreatePost";
import {type Create_PostRequestInput, PostTypeEnum} from "$lib/API/GraphQL/generated";
import type {Actions} from "@sveltejs/kit";

export const actions = {
  default: async (event) => {
    const apollo = getApolloClient();

    const postType = event.url.searchParams.get("type");

    const formData = await event.request.formData();
    const input: Create_PostRequestInput = Object.fromEntries(formData.entries()) as Create_PostRequestInput;

    if(postType !== null) {
      return await createPost(apollo, input, postType.toUpperCase() as PostTypeEnum);
    }


  },
} satisfies Actions;