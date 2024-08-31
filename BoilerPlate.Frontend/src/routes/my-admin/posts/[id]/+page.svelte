<script lang="ts">
  import {onMount} from "svelte";
  import {page} from "$app/stores";
  import PostEditor from "$lib/CMS/PostEditor.svelte";
  import {SITE_NAME} from "$lib/constants";

  let { data: { post } } = $page;

  onMount(() => {
      console.log(post);
  })
</script>

<svelte:head>
  <title>Edit post - {SITE_NAME} </title>
</svelte:head>


<PostEditor post={post}/>