<script lang="ts">
  import type {BlocksData} from "$lib/Content/EditorJS/Blocks/BlocksData";
  import ParagraphPublicBlock from "$lib/Content/EditorJS/Blocks/paragraph/ParagraphPublicBlock.svelte";
  import HeadingPublicBlock from "$lib/Content/EditorJS/Blocks/heading/HeadingPublicBlock.svelte";
  import {onMount} from "svelte";

  let {blocksData}: Props = $props();
  type Props = { blocksData: BlocksData };

  onMount(() => {
    console.log(blocksData);
  })
</script>

{#each blocksData.blocks as block}
  {#if block.type === 'paragraph'}
    <ParagraphPublicBlock block={block.data}/>
  {:else if block.type === 'header'}
    <HeadingPublicBlock block={block.data}/>
  {/if}
{/each}