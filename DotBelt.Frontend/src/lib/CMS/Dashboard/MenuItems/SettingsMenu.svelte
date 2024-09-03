<script lang="ts">
    import PostsIcon from "$lib/CMS/Icones/PostsIcon.svelte";
    import {quadInOut} from "svelte/easing";
    import {slide} from "svelte/transition";
    import SettingsIcon from "$lib/CMS/Icones/SettingsIcon.svelte";

    let hovering : boolean = false;

    let timeout : NodeJS.Timeout;

    function onHoveringLeave() {
        timeout = setTimeout(() => {
            hovering = false;
        }, 500)
    }

    function onHovering() {
        if (timeout) {
            clearTimeout(timeout);
        }
        hovering = true;
    }
</script>
<li class="panel-list-item parent" onmouseover={onHovering} onmouseleave={onHoveringLeave}>
  <a href="/my-admin/posts?type=post" >
    <div class="dashboard-item-container" >
        <span class="icon">
          <SettingsIcon/>
        </span>
      <span class="name">
          Settings
        </span>
    </div>
  </a>
  {#if hovering}
    <ul class="panel-list dropdown" transition:slide="{{ duration: 300, easing: quadInOut }}">
      <li class="panel-list-item">
        <a href="/my-admin/posts/new?type=Post">
          <div class="dashboard-item-container">
          <span class="name">
          Reading
        </span>

          </div>
        </a></li>
      <li class="panel-list-item">
        <a href="/my-admin/taxonomies?type=Post&name=categories">
          <div class="dashboard-item-container">
          <span class="name">
          Roles
        </span>
          </div>
        </a>
      </li>
    </ul>
  {/if}
</li>

<style>


</style>