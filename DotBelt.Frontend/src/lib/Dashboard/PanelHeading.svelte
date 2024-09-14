<script>
    import PostsIcon from "$lib/Utilities/Icons/PostsIcon.svelte";
    import {dashboardData} from "$lib/Dashboard/DashboardStore.svelte";

    let renderFragment;

    $effect(() => {
        renderFragment = $dashboardData.dashboardFragment;
    })
</script>
<div class="panel-heading bg-body-tertiary">
  <div class="left-section">
    <div class="icon">
      <PostsIcon/>
    </div>
    <div class="text-section">
      <div class="title">
        {#if $dashboardData}
          {$dashboardData.title}
        {:else}
          Placeholder title
        {/if}
      </div>
      <div class="subtitle">
        {#if $dashboardData}
          {$dashboardData.subtitle}
        {:else}
          Placeholder subtitle
        {/if}
      </div>
    </div>
  </div>

  <div class="right-section">
      {@render $dashboardData?.dashboardFragment()}
  </div>

</div>

<style>


</style>