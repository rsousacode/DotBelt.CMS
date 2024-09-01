<script lang="ts">
  import {page} from "$app/stores";
  import {SITE_NAME} from "$lib/constants";
  import {onMount} from "svelte";

  let { data: { post } } = $page;

  onMount(() => {
      console.log(post)
  })

</script>

<svelte:head>
  <title>Post- {SITE_NAME} </title>
</svelte:head>

<h2>Post should show here</h2>

{@html post.contentHtml}