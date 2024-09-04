<script>
    import PostEditor from "$lib/CMS/Content/PostEditor.svelte";
    import {SITE_NAME} from "$lib/constants";
    import DashboardContainer from "$lib/CMS/Dashboard/DashboardContainer.svelte";
    import {onMount} from "svelte";
    import { updateDashboardData } from "$lib/CMS/Dashboard/DashboardStore.svelte";


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
