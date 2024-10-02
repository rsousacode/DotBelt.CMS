<script lang="ts">
  import type { Snippet } from 'svelte';
  import ArrowDownRounded from '$lib/Utilities/Icons/ArrowDownRounded.svelte';
  import ArrowUpRounded from '$lib/Utilities/Icons/ArrowUpRounded.svelte';

  let {children, title, cssClasses = ""} : Props = $props();

  type Props =  {children: Snippet, title: string, cssClasses?: string }

  let showChildren = $state(true);

  function onToggleChildren() {
    showChildren = !showChildren;
  }

</script>
<div class="sidebar-item {cssClasses}">
  <div>
    <button type="button" class="clear-button sidebar-item-title" onclick={onToggleChildren} >
      <span>
      {#if showChildren}
        <ArrowUpRounded/>
        {:else}
        <ArrowDownRounded/>
      {/if}
      </span>
      {title}
    </button>
    <div class={!showChildren ? 'hide' : ''}>
      {@render children()}
    </div>


  </div>
</div>