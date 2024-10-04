import { type Maybe, PostStatus, type UploadResponse } from '$lib/API/GraphQL/generated';

export let PostSvelte = {
  id: $state(0),
  title: $state(""),
  status:  $state(PostStatus.Draft),
  relativeUrl:  $state(""),
  description:  $state(""),
  content:   $state("{}"),
  featuredImage: $state(),
  fullUrl: $state(),
  publishDate: $state(new Date().toString()),
  modifiedDate:$state(new Date().toString()),
  featuredImageId: $state()
}