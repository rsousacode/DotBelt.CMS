<script lang="ts">
    import {getContext, onMount} from "svelte";
    import {applyAction, deserialize, enhance} from '$app/forms';
    import {goto, invalidateAll} from "$app/navigation";
    import type {ActionResult} from "@sveltejs/kit";
    import type {Post} from "$lib/GraphQL/generated";
    import EditorJS from "$lib/CMS/EditorJS.svelte";
    import ApolloClientProvider from "$lib/GraphQL/ApolloClientProvider.svelte";

    let {post = $bindable({content: "{}", urlName: ""})}: { post: Post } = $props();

    let editorJs;

    type EditorMode = 'editor' | 'code';

    let currentMode: EditorMode = $state('editor')

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

    async function createPost(event) {
        event.preventDefault();

        let editorData = await editorJs.getData();

        post.content = JSON.stringify(editorData);
        post.title = post.title;
        post.description = post.description;
        post.urlName = post.urlName;

        const formData = new FormData();

        Object.entries(post).forEach(([key, value]) => {
            formData.append(key, value);
        });

        const response = await fetch(event.currentTarget.action, {
            method: 'POST',
            body: formData,
        });

        const result: ActionResult = deserialize(await response.text());
        console.log(result);

        if (result.type === 'success') {
            // rerun all `load` functions, following the successful update
            await invalidateAll();
            await goto("/my-admin/posts/" + result.data.id);
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

    async function handleSubmit(event) {
        if (!post.id) {
            await createPost(event);
        }
    }
</script>

<ApolloClientProvider>
  <form method="POST" action="" onsubmit={handleSubmit}>
    <div class="row">
      <div class="col">
        <div class="editor-container">
          <div>
            <span>url: </span>
            <input class="classy-input permalink-input" onchange={handlePermalinkChanged} type="text"
                   style="color: #494949" placeholder="Permalink here" bind:value={post.urlName}>
          </div>
          <div>
            <input class="classy-input" type="text" style="color: #494949" placeholder="Write your title here"
                   bind:value={post.title}>

          </div>
          {#if currentMode === 'editor'}
            <EditorJS bind:this={editorJs} content={post.content}/>
          {:else}
            <textarea style="width: 100%" rows="10" bind:value={post.content}></textarea>
          {/if}
        </div>
      </div>
      <div class="col-sm-2">

        <button class="btn btn-primary" type="submit">Save</button>
        <button class="btn btn-primary" type="button"
                onclick={onSwitchMode}>{currentMode === 'editor' ? 'Code Editor' : 'Visual Editor'}</button>

        <div class="input-area mt-4">
          <div class="input-group">
            <label class="form-label" for="">Description</label>
            <textarea bind:value={post.description}></textarea>
          </div>
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

    .permalink-input {
        font-size: 16px;
    }

    .editor-container {
        max-width: 650px;
        margin: auto;
    }

    .editor-js {
    }
</style>