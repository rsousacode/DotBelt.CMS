<script lang="ts">

  import type {MenuItem} from "$lib/Dashboard/MenuItems/MenuItem";
  import {quadInOut} from "svelte/easing";
  import {slide} from "svelte/transition";
  import {menuState} from "$lib/Dashboard/MenuItems/MenuState.svelte";
  import { goto } from '$app/navigation';
  import ArrowUpRounded from '$lib/Utilities/Icons/ArrowUpRounded.svelte';
  import ArrowDownRounded from '$lib/Utilities/Icons/ArrowDownRounded.svelte';

  let { menu } : { menu: MenuItem} = $props();

  let nameElement : Element;

  async function onMenuClicked(e: InputEvent) {

    if(menu.items.length <= 0) {
      await goto(menu.href);
      return;
    }

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
      <div class="panel-item-name">
        <a bind:this={nameElement} target={menu.openNewTab ? '_blank' : ''} href={menu.href} class="name" >
          {menu.name}
        </a>
      </div>

      {#if menu.items && menu.items.length && menu.items.length > 0}
        {#if $menuState[menu.name]}
          <ArrowUpRounded/>
        {:else}
          <ArrowDownRounded/>
        {/if}
      {/if}

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

<style>
  .dashboard-item-container {
    display: flex;
    align-items: center;
  }
  .panel-item-name {
    min-width: 130px;
  }
</style>