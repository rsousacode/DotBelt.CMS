<script lang="ts">
import PostsIcon from "$lib/CMS/Icons/PostsIcon.svelte";
import {quadInOut} from "svelte/easing";
import {slide} from "svelte/transition";

let hovering : boolean = false;
</script>
<li class="panel-list-item parent" onmouseover={() => hovering = true} onmouseleave={() => hovering = false}>
  <a href="/my-admin/posts?type=post" >
    <div class="dashboard-item-container" >
        <span class="icon">
          <PostsIcon/>
        </span>
      <span class="name">
          Posts
        </span>
    </div>
  </a>
  {#if hovering}
    <ul class="panel-list dropdown" transition:slide="{{ delay: 500, duration: 150, easing: quadInOut }}">
    <li class="panel-list-item">
      <a href="/my-admin/posts/new?type=Post">
        <div class="dashboard-item-container">
          <span class="name">
          New Post
        </span>

        </div>
      </a></li>
    <li class="panel-list-item">
      <a href="/my-admin/taxonomies?type=Post&name=categories">
        <div class="dashboard-item-container">
          <span class="name">
          Categories
        </span>
        </div>
      </a>
    </li>
    <li class="panel-list-item"><a href="/my-admin/taxonomies?type=Post&name=tags">
      <div class="dashboard-item-container">

        <span class="name">
          Tags
        </span>

      </div>
    </a>
    </li>
  </ul>
  {/if}
</li>

<style>


</style>