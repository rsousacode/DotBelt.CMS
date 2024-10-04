<script lang="ts">
  import {page} from "$app/stores";
  import {SITE_NAME} from "$lib/constants";
  import PublicBlocksRenderer from "$lib/Content/EditorJS/Blocks/PublicBlocksRenderer.svelte";

  let { data: { post, content } } = $page;

</script>

<svelte:head>
  <title>{post.title} - {SITE_NAME} </title>
</svelte:head>

<div class="post-content-container container-fluid">
  <div class="post-title-section">
    <h1>{post.title}</h1>
  </div>

  <div class="post-content">
    <PublicBlocksRenderer blocksData={content} />
  </div>

</div>
