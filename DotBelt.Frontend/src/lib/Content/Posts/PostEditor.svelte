<script lang="ts">
  import { goto } from '$app/navigation';
  import {
    type Maybe,
    type PostResponse,
    type PostResponseInput,
    PostStatus,
    PostTypeEnum,
    type TenantResponse
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
  import { apolloClientStore } from '$lib/API/GraphQL/apolloClientStore';
  import { createPostSvelte } from '$lib/Content/Posts/CreatePost.svelte';
  import { page } from '$app/stores';
  import { editPost } from '$lib/Content/Posts/EditPost.svelte';
  import PreviewIcon from '$lib/Utilities/Icons/PreviewIcon.svelte';
  import PostEditorSidebar from '$lib/Content/Posts/PostEditorSidebar.svelte';

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
    publishDate: new Date(),
    modifiedDate:new Date(),
    featuredImageId: undefined
  });

  type EditorMode = 'editor' | 'code';

  let currentMode: EditorMode = $state('editor');

  let successFeedbackIcon: TransitionalIcon;

  let postDateIsInFuture = $derived(new Date(postData.publishDate) > new Date());


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

    const postResponse = await createPostSvelte(client, postData as PostResponseInput, postType);

    if (postResponse) {
      postData = postResponse ;
      await successFeedbackIcon.triggerComponent2();
      console.log('done????')
      await goto('/my-admin/posts/' + postResponse.id);
    }
  }

  async function editExistingPost() {

    const client = apolloClientStore.getClient();

    if (postData.id) {
      const postResponse = await editPost(client, postData.id, postData as PostResponseInput);
      if (postResponse) {
        postData = postResponse ;
        await successFeedbackIcon.triggerComponent2();
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


  async function handleSubmit(publish: boolean = false) {
    if(publish) {
      postData.status = PostStatus.Published;
    }
    if (!postData.id) {
      await createNewPost();
    } else {
      await editExistingPost();
    }
  }

</script>

{#snippet saveButton()}
  {#if postData.status === PostStatus.Draft}
    <button type="button" onclick={() => handleSubmit(true)} class="btn btn-primary mx-2"> {postDateIsInFuture ? "Schedule" : "Publish"}</button>
  {/if}
  <button class="dashboard-icon" type="button" onclick={() => handleSubmit()}>
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
<form method="POST" action="" onsubmit={() => handleSubmit()}>
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
    <PostEditorSidebar
      bind:postStatus={postData.status}
      bind:featuredImage={postData.featuredImage}
      bind:featuredImageId={postData.featuredImageId}
      bind:publishDate={postData.publishDate}
      bind:description={postData.description}
    />
  </div>
</form>


