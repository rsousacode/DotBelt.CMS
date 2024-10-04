<script lang="ts">
  import type {TableBlockData} from "$lib/Content/EditorJS/Blocks/table/TableBlockData";

  type Props = { block: TableBlockData };
  let { block }: Props = $props();

  let columns = block.content[0];

</script>

<table class="table">
  <thead>
  <tr>
    {#each columns as column}
      <th scope="col">{column}</th>
    {/each}
  </tr>
  </thead>
  <tbody>
  {#each block.content as content, i}
    {#if i !== 0}
      <tr>
        {#each content as row}
          <th scope="row">{row}</th>
        {/each}
      </tr>
    {/if}
  {/each}
  </tbody>
</table>