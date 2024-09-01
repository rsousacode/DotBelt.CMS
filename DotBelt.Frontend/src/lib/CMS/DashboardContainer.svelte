<div class="row">
  <div class="col-sm-2">
    <div class="dashboard-panel">
      <div class="panel-category">
        <ul>
          <li><a href="/my-admin/posts?type=post">Posts</a>
            <ul>
              <li><a href="/my-admin/posts/new?type=Post">New Post</a></li>
              <li><a href="/my-admin/taxonomies?type=Post&name=categories">Categories</a></li>
              <li><a href="/my-admin/taxonomies?type=Post&name=tags">Tags</a></li>
            </ul>
          </li>
          <li><a href="/my-admin/posts?type=Page">Pages</a>
            <ul>
              <li><a href="/my-admin/posts/new?type=Page">New Page</a></li>
              <li><a href="/my-admin/taxonomies?type=Post&name=tags">Tags</a></li>
            </ul>
          </li>
          <li><a href="/my-admin/menus">Visual</a>
            <ul>
              <li><a href="/my-admin/menus">Menus</a></li>
            </ul>
          </li>
          <li><a href="/my-admin/settings/reading">Settings</a>
            <ul>
              <li><a href="">Reading</a></li>
              <li><a href="">Roles</a></li>
            </ul>
          </li>

        </ul>
      </div>

    </div>
  </div>
  <div class="col">
    <slot/>
  </div>
</div>

<style>
    .panel-category {
        display: flex;
        flex-direction: column;
    }

    .dashboard-panel {
        display: flex;
    }

    .panel-item {
        display: flex;
        flex-direction: column;
    }
</style>