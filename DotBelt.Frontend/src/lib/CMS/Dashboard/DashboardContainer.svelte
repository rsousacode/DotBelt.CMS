<script>
    import PostsIcon from "$lib/CMS/Icons/PostsIcon.svelte";
    import PanelHeading from "$lib/CMS/Dashboard/PanelHeading.svelte";
    import Menu from "$lib/CMS/Dashboard/MenuItems/Menu.svelte";
    import PageIcon from "$lib/CMS/Icons/PageIcon.svelte";
    import SettingsIcon from "$lib/CMS/Icons/SettingsIcon.svelte";
    import MediaIcon from "$lib/CMS/Icons/MediaIcon.svelte";
    import DebugIcon from "$lib/CMS/Icons/DebugIcon.svelte";
</script>

<div class="dashboard-container">
  <div class="small-column">
    <div class="dashboard-panel">
      <div class="panel-category">
        <ul class="panel-list">
          <Menu menu={{
              name: "Posts",
              href: "/my-admin/posts?type=post",
              icon: PostsIcon,
              items: [
                {
                  name: "New Post",
                  href: "/my-admin/posts/new?type=Post",
                  icon: undefined,
                  items: []
                },
                {
                  name: "Categories",
                  href: "/my-admin/taxonomies?type=Post&name=categories",
                  icon: undefined,
                  items: []
                },
                {
                  name: "Tags",
                  href: "/my-admin/taxonomies?type=Post&name=tags",
                  icon: undefined,
                  items: []
                },
              ]
          }}/>
          <Menu menu={{
              name: "Pages",
              href: "/my-admin/posts?type=page",
              icon: PageIcon,
              items: [
                {
                  name: "New Post",
                  href: "/my-admin/posts/new?type=page",
                  icon: undefined,
                  items: []
                },
                {
                  name: "Tags",
                  href: "/my-admin/taxonomies?type=page&name=tags",
                  icon: undefined,
                  items: []
                },
              ]
          }}/>
          <Menu menu={{
              name: "Media",
              href: "/my-admin/media",
              icon: MediaIcon,
              items: [
                {
                  name: "Upload Media",
                  href: "/my-admin/posts/new?type=page",
                  icon: undefined,
                  items: []
                },
                {
                  name: "Categories",
                  href: "/my-admin/taxonomies?type=media&name=categories",
                  icon: undefined,
                  items: []
                },
              ]
          }}/>
           <Menu menu={{
              name: "Settings",
              href: "",
              icon: SettingsIcon,
              items: [
                {
                  name: "Reading",
                  href: "/my-admin/settings/reading",
                  icon: undefined,
                  items: []
                },
                {
                  name: "Roles",
                  href: "/my-admin/settings/roles",
                  icon: undefined,
                  items: []
                },
              ]
          }}/>
           <Menu menu={{
              name: "Developer",
              href: "",
              icon: DebugIcon,
              items: [
                {
                  name: "GraphQL",
                  href: "/api/banana",
                  icon: undefined,
                  items: []
                },
                {
                  name: "Swagger",
                  href: "/swagger/index.html",
                  icon: undefined,
                  items: []
                },
              ]
          }}/>


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

    }


</style>