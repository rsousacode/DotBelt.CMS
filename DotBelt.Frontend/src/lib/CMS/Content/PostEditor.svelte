<script lang="ts">
    import {applyAction, deserialize, enhance} from '$app/forms';
    import {goto, invalidateAll} from "$app/navigation";
    import type {ActionResult} from "@sveltejs/kit";
    import type {Post} from "$lib/GraphQL/generated";
    import EditorJS from "$lib/CMS/Content/EditorJS.svelte";
    import ApolloClientProvider from "$lib/GraphQL/ApolloClientProvider.svelte";
    import AceEditor from "$lib/CMS/Content/AceEditor/AceEditor.svelte";
    import {createRawSnippet, onMount} from "svelte";
    import {updateDashboardFragment} from "$lib/CMS/Dashboard/DashboardStore.svelte.js";
    import SaveIcon from "$lib/CMS/Icons/SaveIcon.svelte";
    import TransitionalIcon from "$lib/CMS/Icons/TransitionalIcon.svelte";
    import SuccessIcon from "$lib/CMS/Icons/SuccessIcon.svelte";
    import JsonIcon from "$lib/CMS/Icons/JsonIcon.svelte";
    import BlocksIcon from "$lib/CMS/Icons/BlocksIcon.svelte";


    let {post = $bindable({content: "{}", urlName: ""})}: { post: Post } = $props();

    type EditorMode = 'editor' | 'code';

    let currentMode: EditorMode = $state('editor');

    let successFeedbackIcon : TransitionalIcon;
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

    function handlePermalinkChanged(e) {
        post.urlName = sanitizePermalink(post.urlName);
        console.log(post.urlName)
    }

    async function createPost() {

        post.title = post.title ?? "";
        post.description = post.description ?? "";
        post.urlName = post.urlName ?? "";

        const formData = new FormData();

        Object.entries(post).forEach(([key, value]) => {
            formData.append(key, value);
        });

        const response = await fetch("", {
            method: 'POST',
            body: formData,
        });

        const result: ActionResult = deserialize(await response.text());
        console.log(result);


        if (result.type === 'success') {

            successFeedbackIcon.triggerComponent2();

            if(result.data) {
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
<ApolloClientProvider>
  <form method="POST" action=""  onsubmit={handleSubmit}>
    <div class="row">
      <div class="col">
        <div class="editor-container">
          <div class="editor-container-header">
            <div class="permalink-editor-container">
              <span style="color: #919191;">https://my-website.com/</span>
              <input class="classy-input permalink-input" onchange={handlePermalinkChanged} type="text" placeholder="write-your-url" bind:value={post.urlName}>
            </div>
            <div>
              <input class="classy-input" type="text" style="color: #494949" placeholder="Write your title here"
                     bind:value={post.title}>

            </div>
          </div>

          {#if currentMode === 'editor'}
            <EditorJS  bind:content={post.content}/>
          {:else}
            <AceEditor bind:code={post.content} />
          {/if}

        </div>
      </div>
    </div>
  </form>
</ApolloClientProvider>


<style>
    .classy-input {
        font-size: 24px;
        border: none;
        outline: none;
    }

    .permalink-input::placeholder {
        color: #b8b8b8

    }
    .permalink-input {
        font-size: 16px;
        margin-left: -5px;
        color: #8f8f8f
    }

    .permalink-editor-container {
        margin-bottom: 20px;
    }

    .editor-container-header {
        margin-bottom: 16px;
    }

    .editor-container {
        margin-left: 30px;
    }


    .editor-js {
    }
</style>