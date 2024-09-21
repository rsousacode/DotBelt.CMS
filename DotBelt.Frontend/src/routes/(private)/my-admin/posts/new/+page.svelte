<script>
    import {SITE_NAME} from "$lib/constants";
    import DashboardContainer from "$lib/Dashboard/DashboardContainer.svelte";
    import {onMount} from "svelte";
    import { updateDashboardData } from "$lib/Dashboard/DashboardStore.svelte";
    import PostEditor from "$lib/Content/Posts/PostEditor.svelte";


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
