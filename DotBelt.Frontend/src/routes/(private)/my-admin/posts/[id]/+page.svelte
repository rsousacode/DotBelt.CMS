<script lang="ts">
  import {onMount} from "svelte";
  import {page} from "$app/stores";
  import PostEditor from "$lib/admin/Content/PostEditor.svelte";
  import {SITE_NAME} from "$lib/constants";
  import DashboardContainer from "$lib/admin/Dashboard/DashboardContainer.svelte";
  import {updateDashboardData} from "$lib/admin/Dashboard/DashboardStore.svelte";

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
