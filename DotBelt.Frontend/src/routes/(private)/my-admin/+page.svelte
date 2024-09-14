<script>
    import DashboardContainer from "$lib/Dashboard/DashboardContainer.svelte";
    import {onMount} from "svelte";
    import {setDashboardData} from "$lib/Dashboard/DashboardStore.svelte";

    onMount(() => {
        setDashboardData({title: "Dashboard", subtitle: ""})
    })
</script>

<DashboardContainer>
    <h3>My dashboard</h3>
</DashboardContainer>

