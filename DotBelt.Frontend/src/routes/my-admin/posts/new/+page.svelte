<script>
    import PostEditor from "$lib/CMS/PostEditor.svelte";
    import {SITE_NAME} from "$lib/constants";
</script>
<svelte:head>
  <title>New post - {SITE_NAME} </title>
</svelte:head>

<PostEditor/>