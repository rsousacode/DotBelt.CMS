<script lang="ts">
  import {onMount} from "svelte";
  import {page} from "$app/stores";
  import PostEditor from "$lib/CMS/PostEditor.svelte";
  import {SITE_NAME} from "$lib/constants";
  import DashboardContainer from "$lib/CMS/Dashboard/DashboardContainer.svelte";
  import {setDashboardData} from "$lib/CMS/Dashboard/DashboardStore.svelte";

  let { data: { post } } = $page;


  onMount(() => {
      setDashboardData({title: "Edit post", subtitle: ""})
  })
</script>

<svelte:head>
  <title>Edit post - {SITE_NAME} </title>
</svelte:head>

<DashboardContainer>
  <PostEditor post={post}/>
</DashboardContainer>
