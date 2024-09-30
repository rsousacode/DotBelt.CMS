<script>
    import Uploads from "$lib/Content/Media/Uploads.svelte";
    import {SITE_NAME} from "$lib/constants";
    import UploadsPage from '$lib/Content/Media/UploadsPage.svelte';
</script>

<svelte:head>
  <title>Media - {SITE_NAME} </title>
</svelte:head>

<UploadsPage>
  <Uploads context="Page"/>
</UploadsPage>
