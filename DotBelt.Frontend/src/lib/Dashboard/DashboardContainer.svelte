<script>
    import PostsIcon from "$lib/Utilities/Icons/PostsIcon.svelte";
    import PanelHeading from "$lib/Dashboard/PanelHeading.svelte";
    import Menu from "$lib/Dashboard/MenuItems/Menu.svelte";
    import PageIcon from "$lib/Utilities/Icons/PageIcon.svelte";
    import SettingsIcon from "$lib/Utilities/Icons/SettingsIcon.svelte";
    import MediaIcon from "$lib/Utilities/Icons/MediaIcon.svelte";
    import DebugIcon from "$lib/Utilities/Icons/DebugIcon.svelte";
</script>

<div class="dashboard-container">
  <div class="small-column">
    <div class="dashboard-panel bg-body-tertiary ">
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
                  name: "Users",
                  href: "/my-admin/settings/users",
                  icon: undefined,
                  items: []
                },
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

