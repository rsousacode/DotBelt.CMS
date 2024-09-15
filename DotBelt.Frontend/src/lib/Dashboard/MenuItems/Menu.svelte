<script lang="ts">

  import type {MenuItem} from "$lib/Dashboard/MenuItems/MenuItem";
  import {quadInOut} from "svelte/easing";
  import {slide} from "svelte/transition";
  import {menuState} from "$lib/Dashboard/MenuItems/MenuState.svelte";

  let { menu } : { menu: MenuItem} = $props();

  let nameElement : Element;


  function onMenuClicked(e) {
    if(e.target === nameElement) {
      return;
    }
    $menuState[menu.name] = !$menuState[menu.name];
  }

</script>

<li class="panel-list-item parent" >
  <div class="menu-item" onclick={onMenuClicked}>
    <div class="dashboard-item-container">
      {#if menu.icon }
        <span class="icon">
          <svelte:component this={menu.icon}/>
        </span>
      {/if}
      <a bind:this={nameElement} target={menu.openNewTab ? '_blank' : ''} href={menu.href} class="name" >
          {menu.name}
        </a>
    </div>
  </div>
  {#if menu.items && menu.items.length && menu.items.length > 0}
    {#if $menuState[menu.name] }
      <ul class="panel-list dropdown" transition:slide="{{ duration: 300, easing: quadInOut }}">
        {#each menu.items as item}
          <svelte:self menu={item}/>
        {/each}
      </ul>
    {/if}
  {/if}

</li>