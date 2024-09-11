<script>
    import PostEditor from "$lib/admin/Content/PostEditor.svelte";
    import {SITE_NAME} from "$lib/constants";
    import DashboardContainer from "$lib/admin/Dashboard/DashboardContainer.svelte";
    import {onMount} from "svelte";
    import { updateDashboardData } from "$lib/admin/Dashboard/DashboardStore.svelte";


    onMount(() => {
        updateDashboardData({title: "New Post", subtitle: ""})
    })

</script>
<svelte:head>
  <title>New post - {SITE_NAME} </title>
</svelte:head>
<DashboardContainer>
  <PostEditor/>
</DashboardContainer>
