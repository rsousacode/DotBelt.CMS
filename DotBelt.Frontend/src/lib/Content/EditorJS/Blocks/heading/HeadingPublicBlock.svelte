<script lang="ts">
  import type {HeadingBlockData} from "$lib/Content/EditorJS/Blocks/heading/HeadingBlockData";

  type Props = { block: HeadingBlockData }
  let {block}: Props = $props();

</script>

{#if block.level === 1}
  <h1>{@html block.text}</h1>
{:else if block.level === 2}
  <h2>{@html  block.text}</h2>
{:else if block.level === 3}
  <h3>{@html  block.text}</h3>
{:else if block.level === 4}
  <h4>{@html  block.text}</h4>
{:else if block.level === 5}
  <h5>{@html block.text}</h5>
{:else if block.level === 6}
  <h6>{@html block.text}</h6>
{/if}