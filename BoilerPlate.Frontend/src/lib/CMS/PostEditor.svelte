<script lang="ts">
    import {getContext, onMount} from "svelte";
    import { applyAction, deserialize, enhance } from '$app/forms';
    import { goto, invalidateAll } from "$app/navigation";
    import type { ActionResult } from "@sveltejs/kit";
    import type { Post } from "$lib/GraphQL/generated";
    import EditorJS from "$lib/CMS/EditorJS.svelte";

    let { post = { content: "{}" } }: { post: Post } = $props();

    let editorJs;

    async function createPost(event) {
        event.preventDefault();

        let editorData = await editorJs.getData();
        console.log(editorData);
        post.content = JSON.stringify(editorData);
        post.title = "Test post";
        post.description = "Test description";
        post.urlName = "test";

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

    async function handleSubmit(event) {
        if (!post.id) {
            await createPost(event);
        }
    }
</script>

<form method="POST" action="" onsubmit={handleSubmit}>
  <div class="row">
    <div class="col">
      <div class="editor-container">
        <h1 style="color: #494949" contenteditable="">Write your title here</h1>
        <EditorJS bind:this={editorJs} content={post.content} />
      </div>
    </div>
    <div class="col-sm-2">
      <button class="btn btn-primary" type="submit">Save</button>
    </div>
  </div>
</form>

<style>
    .editor-container {
        max-width: 650px;
        margin: auto;
    }
    .editor-js {
    }
</style>