<script>
    import PostsIcon from "$lib/Utilities/Icons/PostsIcon.svelte";
    import PanelHeading from "$lib/Dashboard/PanelHeading.svelte";
    import Menu from "$lib/Dashboard/MenuItems/Menu.svelte";
    import PageIcon from "$lib/Utilities/Icons/PageIcon.svelte";
    import SettingsIcon from "$lib/Utilities/Icons/SettingsIcon.svelte";
    import MediaIcon from "$lib/Utilities/Icons/MediaIcon.svelte";
    import DebugIcon from "$lib/Utilities/Icons/DebugIcon.svelte";
    import MenuIcon from '$lib/Utilities/Icons/MenuIcon.svelte';
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
                  openNewTab: false,
                  items: []
                },
                {
                  name: "Categories",
                  href: "/my-admin/taxonomies?type=Post&name=categories",
                  icon: undefined,
                  openNewTab: false,
                  items: []
                },
                {
                  name: "Tags",
                  href: "/my-admin/taxonomies?type=Post&name=tags",
                  icon: undefined,
                  openNewTab: false,
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
                  openNewTab: false,
                  items: []
                },
                {
                  name: "Tags",
                  href: "/my-admin/taxonomies?type=page&name=tags",
                  icon: undefined,
                  openNewTab: false,
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
                  openNewTab: false,
                  items: []
                },
                {
                  name: "Categories",
                  href: "/my-admin/taxonomies?type=media&name=categories",
                  icon: undefined,
                  openNewTab: false,
                  items: []
                },
              ]
          }}/>
          <Menu menu={{
              name: "Menus",
              href: "/my-admin/visual/menus",
              icon: MenuIcon,
              openNewTab: false,
              items: []
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
                  openNewTab: false,
                  items: []
                },
                {
                  name: "Reading",
                  href: "/my-admin/settings/reading",
                  icon: undefined,
                  openNewTab: false,
                  items: []
                },
                {
                  name: "Roles",
                  href: "/my-admin/settings/roles",
                  icon: undefined,
                  openNewTab: false,
                  items: []
                },
              ]
          }}/>
          <Menu menu={{
              name: "Developer",
              href: "",
              icon: DebugIcon,
              openNewTab: false,
              items: [
                {
                  name: "GraphQL",
                  href: "/api/banana",
                  icon: undefined,
                  openNewTab: false,
                  items: []
                },
                {
                  name: "Swagger",
                  href: "/swagger/index.html",
                  icon: undefined,
                  openNewTab: false,
                  items: []
                },
                {
                  name: "Email Dev",
                  href: "http://localhost:5925",
                  icon: undefined,
                  openNewTab: true,
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

