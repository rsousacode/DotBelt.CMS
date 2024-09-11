<script>
    import PostsIcon from "$lib/admin/Icons/PostsIcon.svelte";
    import {dashboardData} from "$lib/admin/Dashboard/DashboardStore.svelte";

    let renderFragment;

    $effect(() => {
        renderFragment = $dashboardData.dashboardFragment;
    })
</script>
<div class="panel-heading">
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


    .panel-heading {
        background-color: #45279f;
        width: 100%;
        padding-left: 20px;
        padding-top: 10px;
        padding-bottom: 10px;
        display: flex;
        align-items: center;
        justify-content: space-between;
    }

    .panel-heading .text-section {
        padding-left: 20px;
    }

    .panel-heading .text-section .title {
        font-size: 20px;
        color: white;
    }


    .panel-heading .left-section {
        display: flex;
        justify-content: center;
        align-items: center;
    }


    .panel-heading .right-section {
        display: flex;
        justify-content: center;
        align-items: center;
        padding-right: 30px;
    }

    .panel-heading .icon {
        border-right: 1px solid gray;
        font-size: 32px;
        color: #ffffff;
        padding-right: 15px;
    }

    .panel-heading .text-section .subtitle {
        font-size: 16px;
        color: #adadad;
        margin-bottom: 5px;
    }

</style>