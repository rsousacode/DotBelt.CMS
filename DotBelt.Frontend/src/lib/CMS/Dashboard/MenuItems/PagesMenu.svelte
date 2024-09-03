<script lang="ts">
    import {quadInOut} from "svelte/easing";
    import {slide} from "svelte/transition";
    import PageIcon from "$lib/CMS/Icons/PageIcon.svelte";

    let hovering: boolean = false;

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
  <a href="/my-admin/posts?type=page">
    <div class="dashboard-item-container">
        <span class="icon">
          <PageIcon/>
        </span>
      <span class="name">
          Pages
        </span>
    </div>
  </a>
  {#if hovering}
    <ul class="panel-list dropdown" transition:slide="{{ delay: 500, duration: 150, easing: quadInOut }}">
      <li class="panel-list-item">
        <a href="/my-admin/posts/new?type=page">
          <div class="dashboard-item-container">
          <span class="name">
          New Page
        </span>

          </div>
        </a></li>

      <li class="panel-list-item">
        <a href="/my-admin/taxonomies?type=page&name=tags">
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