<script lang="ts">
  import {applyAction, deserialize} from '$app/forms';
  import {goto, invalidateAll} from "$app/navigation";
  import type {ActionResult} from "@sveltejs/kit";
  import type {Post} from "$lib/API/GraphQL/generated";
  import {onMount} from "svelte";
  import {updateDashboardFragment} from "$lib/Dashboard/DashboardStore.svelte.js";
  import SaveIcon from "$lib/Utilities/Icons/SaveIcon.svelte";
  import TransitionalIcon from "$lib/Utilities/Icons/TransitionalIcon.svelte";
  import SuccessIcon from "$lib/Utilities/Icons/SuccessIcon.svelte";
  import JsonIcon from "$lib/Utilities/Icons/JsonIcon.svelte";
  import BlocksIcon from "$lib/Utilities/Icons/BlocksIcon.svelte";
  import EditorJS from "$lib/Content/EditorJS/EditorJS.svelte";
  import AceEditor from "$lib/Utilities/AceEditor.svelte";


  let {post = $bindable({content: "{}", relativeUrl: ""})}: { post: Post } = $props();

  type EditorMode = 'editor' | 'code';

  let currentMode: EditorMode = $state('editor');

  let successFeedbackIcon: TransitionalIcon;
  onMount(() => {
    updateDashboardFragment(saveButton);
  })


  function sanitizePermalink(permalink: string) {
    // Replace spaces with hyphens
    permalink = permalink.replace(/\s+/g, "-");

    // Remove any characters that are not letters, numbers, or hyphens
    permalink = permalink.replace(/[^a-zA-Z0-9-/]/g, "");

    permalink = permalink.replace(/^-+|-+$/g, "");
    // Convert the permalink to lowercase
    permalink = permalink.toLowerCase();

    return permalink;
  }

  function handlePermalinkChanged() {
    post.relativeUrl = sanitizePermalink(post.relativeUrl);
  }

  async function createPost() {

    post.title = post.title ?? "";
    post.description = post.description ?? "";
    post.relativeUrl= post.relativeUrl?? "";

    const formData = new FormData();

    Object.entries(post).forEach(([key, value]) => {
      formData.append(key, value);
    });

    const response = await fetch("", {
      method: 'POST',
      body: formData,
      credentials: 'include',
    });

    const result: ActionResult = deserialize(await response.text());

    if (result.type === 'success') {

      successFeedbackIcon.triggerComponent2();

      if (result.data) {
        await invalidateAll();
        await goto("/my-admin/posts/" + result.data.id);
      }
    }

    await applyAction(result);
  }

  function onSwitchMode() {
    if (currentMode === 'code') {
      currentMode = 'editor';
    } else {
      currentMode = 'code';
    }
  }

  async function handleSubmit() {
    if (!post.id) {
      await createPost();
    }
  }
</script>

{#snippet saveButton()}
  <button class="dashboard-icon" type="button" onclick={handleSubmit}>
    <TransitionalIcon
      bind:this={successFeedbackIcon}
      component1={SaveIcon}
      component2={SuccessIcon}/>
  </button>
  <button class="dashboard-icon" onclick={onSwitchMode}>
    {#if currentMode === 'editor'}
      <JsonIcon/>
    {:else}
      <BlocksIcon/>
    {/if}
  </button>
{/snippet}
<form method="POST" action="" onsubmit={handleSubmit}>
  <div class="post-editor-container">
    <div class="editor-container">
      <div class="editor-container-header">
        <div class="permalink-editor-container">
          <span style="color: #919191;">https://my-website.com/</span>
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
        <EditorJS bind:content={post.content}/>
      {:else}
        <AceEditor bind:code={post.content}/>
      {/if}

    </div>
    <div class="post-editor-sidebar-container">
      <div class="sidebar-item">
        <div class="sidebar-item-title">
          Featured image
        </div>
        <div class="featured-image">
          <img src="/images/placeholder-image.png" alt="">
        </div>
      </div>
    </div>
  </div>
</form>

<style>
  .featured-image > img {
      max-width: 100%;
  }
</style>