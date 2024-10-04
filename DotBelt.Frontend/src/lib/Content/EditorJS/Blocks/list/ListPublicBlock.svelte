<script lang="ts">
  import type { ListBlockData } from '$lib/Content/EditorJS/Blocks/list/ListBlockData';

  type Props = { block: ListBlockData };
  let { block }: Props = $props();

</script>

{#if block.style === 'ordered'}
  <ol>
    {#each block.items as item}
      <li>
        {item}
      </li>
    {/each}
  </ol>
{:else}
  <ul>
    {#each block.items as item}
      <li>
        {item}
      </li>
    {/each}
  </ul>
{/if}
