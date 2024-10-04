import { type Maybe, PostStatus, type UploadResponse } from '$lib/API/GraphQL/generated';

export class PostSvelte {
  id: number = $state(0);
  title: string = $state("");
  status: PostStatus = $state(PostStatus.Draft);
  relativeUrl: string = $state("")
  description: Maybe<string> | undefined = $state("")
  content:  Maybe<string> | undefined  = $state("{}")
  featuredImage: Maybe<UploadResponse> | undefined = $state()
  fullUrl = $state()
  publishDate = $state(new Date().toString())
  modifiedDate = $state(new Date().toString())
  featuredImageId :  Maybe<number> | undefined = $state();
}