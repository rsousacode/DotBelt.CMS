<script lang="ts">
  import { goto } from '$app/navigation';
  import {
    type PostResponse,
    type PostResponseInput, PostStatus,
    PostTypeEnum, type TenantResponse,
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
  import { createPost } from '$lib/Content/Posts/CreatePost';
  import { page } from '$app/stores';
  import { editPost } from '$lib/Content/Posts/EditPost';
  import PostEditorSidebarItem from '$lib/Content/Posts/PostEditorSidebarItem.svelte';
  import PreviewIcon from '$lib/Utilities/Icons/PreviewIcon.svelte';
  import HTML5DateTimeInput from '$lib/Utilities/HTML5DateTimeInput.svelte';

  type Props = { post?: PostResponse, tenant: TenantResponse }

  let {
    tenant,
    post = $bindable(
      {
        title: '',
        status: PostStatus.Draft,
        relativeUrl: '',
        description: '',
        content: '{}',
        featuredImage: undefined
      },

    )
  }: Props = $props();

  type EditorMode = 'editor' | 'code';

  let currentMode: EditorMode = $state('editor');

  let uploadsPanel: Uploads;

  let successFeedbackIcon: TransitionalIcon;

  let featuredImageElement: HTMLImageElement;

  onMount(() => {
    updateDashboardFragment(saveButton);
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
    post.relativeUrl = sanitizePermalink(post.relativeUrl);
  }

  async function createNewPost() {

    const client = apolloClientStore.getClient();

    const postType = $page.url.searchParams.get('type')?.toUpperCase() as PostTypeEnum;

    const postResponse = await createPost(client, post as PostResponseInput, postType);

    if (postResponse) {
      post.relativeUrl = postResponse.relativeUrl;
      post.fullUrl = postResponse.fullUrl;
      post.publishDate = postResponse.publishDate;
      post.modifiedDate = postResponse.modifiedDate;

      successFeedbackIcon.triggerComponent2();
      await goto('/my-admin/posts/' + postResponse.id);
    }
  }

  async function editExistingPost() {

    const client = apolloClientStore.getClient();


    if (post.id) {
      const postResponse = await editPost(client, post.id, post as PostResponseInput);
      console.log('post response [edit]', postResponse);

      if (postResponse) {
        post.relativeUrl = postResponse.relativeUrl;
        post.fullUrl = postResponse.fullUrl;
        post.publishDate = postResponse.publishDate;
        post.modifiedDate = postResponse.modifiedDate;
        successFeedbackIcon.triggerComponent2();
      }
    } else {
      console.error('Post has no id');
    }
  }

  function onSwitchMode() {
    if(post.content) {
      const parsedToObject = JSON.parse(post.content);
      post.content = JSON.stringify(parsedToObject ,null,2);
    }

    if (currentMode === 'code') {
      currentMode = 'editor';
    } else {
      currentMode = 'code';
    }
  }


  function onFeaturedImageSelected(upload: UploadResponse) {
    post.featuredImage = { ...upload };
    post.featuredImageId = upload.id;
    featuredImageElement.src = `/${upload.relativeUrl}`;
  }

  async function openFeatureImageSelection() {
    await uploadsPanel.open(onFeaturedImageSelected, post.featuredImage?.id);
  }

  async function handleSubmit() {
    if (!post.id) {
      await createNewPost();
    } else {
      await editExistingPost();
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
  <button style="font-size: 29px" class="dashboard-icon">
    <PreviewIcon />
  </button>
{/snippet}
<form method="POST" action="" onsubmit={handleSubmit}>
  <div class="post-editor-container">
    <div class="editor-container">
      <div class="editor-container-header">
        <div class="permalink-editor-container">
          <span style="color: #919191;">{tenant.fullUrl}/</span>
          <input class="classy-input permalink-input" onchange={handlePermalinkChanged} type="text"
                 placeholder="write-your-url" bind:value={post.relativeUrl}>
        </div>
        <div>
          <input class="classy-input" type="text" placeholder="Write your title here"
                 bind:value={post.title}>

        </div>
      </div>
      <hr>

      {#if currentMode === 'editor'}
        <EditorJS bind:content={post.content} />
      {:else}
        <AceEditor bind:code={post.content} />
      {/if}

    </div>
    <div class="post-editor-sidebar-container">
      <PostEditorSidebarItem title="Options">
        <div class="form-control mt-2">
          <label  class="form-label mt-3" for="">Publishing status</label>
          <select bind:value={post.status} class="form-select" aria-label="publish status">
            <option value={PostStatus.Draft}>Draft</option>
            <option value={PostStatus.Published}>Published</option>
          </select>
          <div class="mt-4 mb-3">
            <label class="form-label" for="publish-date">Publishing Date</label>
            <HTML5DateTimeInput bind:dateTime={post.publishDate}></HTML5DateTimeInput>
          </div>
        </div>
      </PostEditorSidebarItem>
      <PostEditorSidebarItem cssClasses="featured-image-container" title="Featured image">
          <button type="button" class="featured-image clear-button" onclick={openFeatureImageSelection}>
            <img bind:this={featuredImageElement}
                 src={post?.featuredImage?.relativeUrl ? `/${post.featuredImage.relativeUrl}` : "/images/placeholder-image.png"}
                 alt="">
          </button>

      </PostEditorSidebarItem>

      <PostEditorSidebarItem title="Description">
        <textarea
          bind:value={post.description}
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