<script>
    import DashboardContainer from "$lib/admin/Dashboard/DashboardContainer.svelte";
    import {SITE_NAME} from "$lib/constants";
    import {onMount} from "svelte";
    import {setDashboardData} from "$lib/admin/Dashboard/DashboardStore.svelte";


    onMount(() => {
        setDashboardData({title: "Users", subtitle: "User management"})
    })

</script>

<svelte:head>
  <title>Users - {SITE_NAME} </title>
</svelte:head>

<DashboardContainer>
  <p>Users page</p>
</DashboardContainer>
