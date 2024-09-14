<script lang="ts">
  import {onMount} from "svelte";
  import {page} from "$app/stores";
  import {SITE_NAME} from "$lib/constants";
  import DashboardContainer from "$lib/Dashboard/DashboardContainer.svelte";
  import {updateDashboardData} from "$lib/Dashboard/DashboardStore.svelte";
  import PostEditor from "$lib/Content/Components/PostEditor.svelte";

  let { data: { post } } = $page;


  onMount(() => {
      updateDashboardData({title: "Edit post", subtitle: ""})
  })
</script>

<svelte:head>
  <title>Edit post - {SITE_NAME} </title>
</svelte:head>

<DashboardContainer>
  <PostEditor post={post}/>
</DashboardContainer>
