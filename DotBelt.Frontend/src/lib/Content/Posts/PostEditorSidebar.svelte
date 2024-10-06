<script lang="ts">
  import {
    type DotBeltQuery,
    type InputMaybe,
    type Maybe,
    PostStatus,
    type UploadResponse,
    type UploadResponseInput
  } from '$lib/API/GraphQL/generated';
  import PostEditorSidebarItem from '$lib/Content/Posts/PostEditorSidebarItem.svelte';
  import HTML5DateTimeInput from '$lib/Utilities/HTML5DateTimeInput.svelte';

  import Uploads from '$lib/Content/Media/Uploads.svelte';
  import { gql } from '@apollo/client/core';
  import { apolloClientStore } from '$lib/API/GraphQL/apolloClientStore';
  import { InternalCrops } from '$lib/API/InternalCrops';

  type Props = {
    postStatus: PostStatus,
    featuredImage: InputMaybe<UploadResponseInput> | undefined ,
    featuredImageId: Maybe<number> | undefined,
    publishDate: string,
    description:  InputMaybe<string> | undefined
  };

  let {
    postStatus = $bindable(),
    featuredImage = $bindable(),
    featuredImageId = $bindable(),
    publishDate = $bindable(),
    description = $bindable()
  }: Props = $props();


  let featuredImageElement: HTMLImageElement;
  let uploadsPanel: Uploads;


  let currentDate = $derived(new Date());

  let postDate = $derived(new Date(publishDate));

  let postDateIsInFuture = $derived(postDate > currentDate);

  async function onFeaturedImageSelected(upload: UploadResponse) {
    featuredImageId = upload.id;

    const query = gql`
    query getThumbnail($id: Int!, $cropName: String!) {
      thumbnailByUploadId(uploadId: $id, cropName: $cropName) {
        relativeUrl
        uploadId
      }
    }
`;

    const client = apolloClientStore.getClient()

    const {data: {thumbnailByUploadId}} = await client.query<DotBeltQuery>(
      {
        query: query,
        variables: { cropName: InternalCrops.FeaturedImageCrop, id: upload.id }
      }
    )

    const thumbnail = thumbnailByUploadId[0];

    console.log(thumbnail);

    featuredImageElement.src = `/${thumbnail.relativeUrl}`;
  }

  async function openFeatureImageSelection() {
    await uploadsPanel.open(onFeaturedImageSelected, featuredImage?.id);
  }

  function onPublishDateChange() {
    if (postDateIsInFuture) {
      if (postStatus === PostStatus.Published) {
        postStatus = PostStatus.Scheduled;
      }
    } else {
      if (postStatus === PostStatus.Scheduled) {
        postStatus = PostStatus.Published;
      }
    }
  }

</script>

<Uploads bind:this={uploadsPanel} context="OneMediaSelection" />

<div class="post-editor-sidebar-container">
  <PostEditorSidebarItem title="Options">
    <div class="form-control mt-2">
      <label class="form-label mt-3" for="">Publishing status</label>
      <select bind:value={postStatus} class="form-select" aria-label="publish status">
        <option value={PostStatus.Draft}>Draft</option>

        {#if !postDateIsInFuture}
          <option value={PostStatus.Published}>Published</option>
        {:else}
          <option value={PostStatus.Scheduled}>Scheduled</option>
        {/if}
      </select>
      <div class="mt-4 mb-3">
        <label class="form-label" for="publish-date">Publishing Date</label>
        <HTML5DateTimeInput onchange={onPublishDateChange}
                            bind:dateTime={publishDate}></HTML5DateTimeInput>
      </div>
    </div>
  </PostEditorSidebarItem>
  <PostEditorSidebarItem cssClasses="featured-image-container" title="Featured image">
    <button type="button" class="featured-image clear-button" onclick={openFeatureImageSelection}>
      <img bind:this={featuredImageElement}
           src={featuredImage?.relativeUrl ? `/${featuredImage.relativeUrl}` : "/images/placeholder-image.png"}
           alt="">
    </button>

  </PostEditorSidebarItem>

  <PostEditorSidebarItem title="Description">
        <textarea
          bind:value={description}
          rows="3" class="form-control mt-2"
          id="exampleInputEmail1"
          placeholder="Short description about the post"
          aria-describedby="emailHelp">

        </textarea>
  </PostEditorSidebarItem>

</div>

<style>
  .featured-image {
    margin-top: 10px;
  }

  .featured-image > img {
    max-width: 100%;
    border-radius: 5px;
  }
</style>