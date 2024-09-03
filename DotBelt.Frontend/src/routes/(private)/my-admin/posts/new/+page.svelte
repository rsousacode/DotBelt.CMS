<script>
    import PostEditor from "$lib/CMS/PostEditor.svelte";
    import {SITE_NAME} from "$lib/constants";
    import DashboardContainer from "$lib/CMS/Dashboard/DashboardContainer.svelte";
    import {onMount} from "svelte";
    import {setDashboardData} from "$lib/CMS/Dashboard/DashboardStore.svelte";


    onMount(() => {
        setDashboardData({title: "New Post", subtitle: ""})


    })
</script>
<svelte:head>
  <title>New post - {SITE_NAME} </title>
</svelte:head>
<DashboardContainer>
  <PostEditor/>
</DashboardContainer>
