<div class="row">
  <div class="col-sm-2">
    <div class="dashboard-panel">
      <div class="panel-category">
        <div class="panel-category-name">
          <span class="category-name">Posts</span>
        </div>
        <div class="panel-item">
          <a href="/my-admin/posts/new">New Post</a>
          <a href="/my-admin/posts">Posts</a>
          <a href="/my-admin/taxonomies?post_type=posts&name=categories">Categories</a>
          <a href="/my-admin/taxonomies?post_type=posts&name=tags">Tags</a>
        </div>
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