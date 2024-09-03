<script>
    import PostsIcon from "$lib/CMS/Icons/PostsIcon.svelte";
    import PostsMenu from "$lib/CMS/Dashboard/MenuItems/PostsMenu.svelte";
    import PagesMenu from "$lib/CMS/Dashboard/MenuItems/PagesMenu.svelte";
    import SettingsMenu from "$lib/CMS/Dashboard/MenuItems/SettingsMenu.svelte";
    import {dashboardData} from "$lib/CMS/Dashboard/DashboardStore.svelte";
    import PanelHeading from "$lib/CMS/Dashboard/PanelHeading.svelte";
</script>

<div class="dashboard-container">
  <div class="small-column">
    <div class="dashboard-panel">
      <div class="panel-category">
        <ul class="panel-list">
          <PostsMenu/>
          <PagesMenu/>
          <SettingsMenu/>
        </ul>
      </div>

    </div>
  </div>
  <div class="large-column">
    <PanelHeading/>
    <div class="panel-content-container">
      <slot/>
    </div>
  </div>
</div>

<style>

    .dashboard-container {
        display: flex;
        height: 100%;
    }

    .small-column {
        min-width: 224px;
        height: 100%;
    }

    .large-column {
        width: 100%;
        display: flex;
        flex-direction: column;
    }

    .panel-content-container {
        margin-left: 20px;
        margin-right: 20px;
        /* margin-bottom: 20px; */
        margin-top: 20px;
        background: white;
        padding: 13px;
        flex-direction: column;
        box-shadow: 0px 1px 10px 1px rgba(110, 106, 200, 0.08);
        max-height: 100% !important;
        overflow-y: scroll;
    }

    .panel-category {
        display: flex;
        flex-direction: column;
        width: 100%;
    }

    .dashboard-panel {
        display: flex;
        background-color: #291569;
        height: 100%;
        padding-top: 24px;
        width: 100%;
    }

    .panel-list-item > a {
        text-decoration: none;
        color: white;
    }

    .panel-list {
        list-style-type: none;
        padding: 0;
        z-index: 2;

    }



</style>