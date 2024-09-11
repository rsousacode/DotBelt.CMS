<script lang="ts">

  import type {MenuItem} from "$lib/admin/Dashboard/MenuItems/MenuItem";
  import {quadInOut} from "svelte/easing";
  import {slide} from "svelte/transition";

  let { menu } : { menu: MenuItem} = $props();

  let hovering: boolean = $state(false);

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
  <a href={menu.href}>
    <div class="dashboard-item-container">
      {#if menu.icon }
        <span class="icon">
          <svelte:component this={menu.icon}/>
        </span>
      {/if}
      <span class="name">
          {menu.name}
        </span>
    </div>
  </a>
  {#if menu.items && menu.items.length && menu.items.length > 0}
    {#if hovering}
      <ul class="panel-list dropdown" transition:slide="{{ duration: 300, easing: quadInOut }}">
        {#each menu.items as item}
          <svelte:self menu={item}/>
        {/each}
      </ul>
    {/if}
  {/if}

</li>