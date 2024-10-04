<script lang="ts">
  import { goto } from '$app/navigation';
  import {
    type Maybe,
    type PostResponse,
    type PostResponseInput,
    PostStatus,
    PostTypeEnum,
    type TenantResponse,
    type UploadResponse
  } from '$lib/API/GraphQL/generated';
  import { onMount } from 'svelte';
  import { updateDashboardFragment } from '$lib/Dashboard/DashboardStore.svelte.js';
  import SaveIcon from '$lib/Utilities/Icons/SaveIcon.svelte';
  import TransitionalIcon from '$lib/Utilities/Icons/TransitionalIcon.svelte';
  import SuccessIcon from '$lib/Utilities/Icons/SuccessIcon.svelte';
  import JsonIcon from '$lib/Utilities/Icons/JsonIcon.svelte';
  import BlocksIcon from '$lib/Utilities/Icons/BlocksIcon.svelte';
  import EditorJS from '$lib/Content/EditorJS/EditorJS.svelte';
  import AceEditor from '$lib/Utilities/AceEditor.svelte';
  import Uploads from '$lib/Content/Media/Uploads.svelte';
  import { apolloClientStore } from '$lib/API/GraphQL/apolloClientStore';
  import { createPostSvelte } from '$lib/Content/Posts/CreatePost.svelte';
  import { page } from '$app/stores';
  import { editPost } from '$lib/Content/Posts/EditPost.svelte';
  import PostEditorSidebarItem from '$lib/Content/Posts/PostEditorSidebarItem.svelte';
  import PreviewIcon from '$lib/Utilities/Icons/PreviewIcon.svelte';
  import HTML5DateTimeInput from '$lib/Utilities/HTML5DateTimeInput.svelte';

  type Props = { post?: Maybe<PostResponse>, tenant: TenantResponse }

  let {
    tenant,
    post
  }: Props = $props();

  let postData : PostResponseInput = $state({
    id: 0,
    title: "",
    status:  PostStatus.Draft,
    relativeUrl:  "",
    description:  "",
    content:   "{}",
    featuredImage: {relativeUrl: ""},
    fullUrl: undefined,
    publishDate: new Date(), // Reactive yay
    modifiedDate:new Date(),
    featuredImageId: undefined
  });

  type EditorMode = 'editor' | 'code';

  let currentMode: EditorMode = $state('editor');

  let uploadsPanel: Uploads;

  let successFeedbackIcon: TransitionalIcon;

  let featuredImageElement: HTMLImageElement;

  let currentDate = $derived(new Date());

  let postDate = $derived(new Date(postData.publishDate));

  let postDateIsInFuture = $derived(postDate > currentDate);

  onMount(() => {
    updateDashboardFragment(saveButton);

    if(post) {
      postData = post;
    }

  });


  function sanitizePermalink(permalink: string) {
    // Replace spaces with hyphens
    permalink = permalink.replace(/\s+/g, '-');

    // Remove any characters that are not letters, numbers, or hyphens
    permalink = permalink.replace(/[^a-zA-Z0-9-/]/g, '');

    permalink = permalink.replace(/^-+|-+$/g, '');
    // Convert the permalink to lowercase
    permalink = permalink.toLowerCase();

    return permalink;
  }

  function handlePermalinkChanged() {
    postData.relativeUrl = sanitizePermalink(postData.relativeUrl);
  }

  async function createNewPost() {

    const client = apolloClientStore.getClient();

    const postType = $page.url.searchParams.get('type')?.toUpperCase() as PostTypeEnum;

    console.log('postdata', postData);
    console.log('sna', $state.snapshot(postData));


    const postResponse = await createPostSvelte(client, postData as PostResponseInput, postType);

    if (postResponse) {
      postData = postResponse ;
      successFeedbackIcon.triggerComponent2();
      await goto('/my-admin/posts/' + postResponse.id);
    }
  }

  async function editExistingPost() {

    const client = apolloClientStore.getClient();

    if (postData.id) {
      const postResponse = await editPost(client, postData.id, postData as PostResponseInput);
      if (postResponse) {
        postData = postResponse ;
        console.log(postResponse)
        successFeedbackIcon.triggerComponent2();
      }
    }
  }

  function onSwitchMode() {
    if (postData.content) {
      const parsedToObject = JSON.parse(postData.content);
      postData.content = JSON.stringify(parsedToObject, null, 2);
    }

    if (currentMode === 'code') {
      currentMode = 'editor';
    } else {
      currentMode = 'code';
    }
  }


  function onFeaturedImageSelected(upload: UploadResponse) {
    postData.featuredImage = { ...upload };
    postData.featuredImageId = upload.id;
    featuredImageElement.src = `/${upload.relativeUrl}`;
  }

  async function openFeatureImageSelection() {
    await uploadsPanel.open(onFeaturedImageSelected, postData.featuredImage?.id);
  }

  async function handleSubmit() {
    if (!postData.id) {
      await createNewPost();
    } else {
      await editExistingPost();
    }
  }

  function onPublishDateChange() {
    if (postDateIsInFuture) {
      if(postData.status === PostStatus.Published) {
        postData.status = PostStatus.Scheduled;
      }
    } else {
      if (postData.status === PostStatus.Scheduled) {
        postData.status = PostStatus.Published;
      }
    }
  }

</script>

{#snippet saveButton()}
  <button class="dashboard-icon" type="button" onclick={handleSubmit}>
    <TransitionalIcon
      bind:this={successFeedbackIcon}
      component1={SaveIcon}
      component2={SuccessIcon} />
  </button>
  <button style="font-size: 29px" class="dashboard-icon" onclick={onSwitchMode}>
    {#if currentMode === 'editor'}
      <JsonIcon />
    {:else}
      <BlocksIcon />
    {/if}
  </button>
  <a href={`/${postData.relativeUrl}`} target="_blank" style="font-size: 29px" class="dashboard-icon clear-button">
    <PreviewIcon />
  </a>
{/snippet}
<form method="POST" action="" onsubmit={handleSubmit}>
  <div class="post-editor-container">
    <div class="editor-container">
      <div class="editor-container-header">
        <div class="permalink-editor-container">
          <span style="color: #919191;">{tenant.fullUrl}/</span>
          <input class="classy-input permalink-input" onchange={handlePermalinkChanged} type="text"
                 placeholder="write-your-url" bind:value={postData.relativeUrl}>
        </div>
        <div>
          <input class="classy-input" type="text" placeholder="Write your title here"
                 bind:value={postData.title}>

        </div>
      </div>
      <hr>

      {#if currentMode === 'editor'}
        <EditorJS bind:content={postData.content} />
      {:else}
        <AceEditor bind:code={postData.content} />
      {/if}

    </div>
    <div class="post-editor-sidebar-container">
      <PostEditorSidebarItem title="Options">
        <div class="form-control mt-2">
          <label class="form-label mt-3" for="">Publishing status</label>
          <select bind:value={postData.status} class="form-select" aria-label="publish status">
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
                                bind:dateTime={postData.publishDate}></HTML5DateTimeInput>
          </div>
        </div>
      </PostEditorSidebarItem>
      <PostEditorSidebarItem cssClasses="featured-image-container" title="Featured image">
        <button type="button" class="featured-image clear-button" onclick={openFeatureImageSelection}>
          <img bind:this={featuredImageElement}
               src={postData?.featuredImage?.relativeUrl ? `/${postData.featuredImage.relativeUrl}` : "/images/placeholder-image.png"}
               alt="">
        </button>

      </PostEditorSidebarItem>

      <PostEditorSidebarItem title="Description">
        <textarea
          bind:value={postData.description}
          rows="3" class="form-control mt-2"
          id="exampleInputEmail1"
          placeholder="Short description about the post"
          aria-describedby="emailHelp">

        </textarea>
      </PostEditorSidebarItem>

    </div>
  </div>
</form>

<Uploads bind:this={uploadsPanel} context="OneMediaSelection" />

<style>
  .featured-image {
    margin-top: 10px;
  }

  .featured-image > img {
    max-width: 100%;
    border-radius: 5px;
  }
</style>