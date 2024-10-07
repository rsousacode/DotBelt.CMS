<script lang="ts">
  import type { SupportedEmbed } from '$lib/Content/EditorJS/Blocks/embed/EmbedProps';

  let {embedType, relevantPart} : Props = $props();

  type Props = {embedType: SupportedEmbed, relevantPart: string}
</script>

{#if embedType === 'youtube'}
  <iframe
    title="embed"
    class={relevantPart ? "" : "hidden"}
    width="500"
    height="281"
    src="https://www.youtube.com/embed/{relevantPart}"
    referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
{:else if embedType === 'tiktok'}
  <iframe
    title="embed"
    width="500"
    height="281"
    src="https://www.tiktok.com/player/v1/{relevantPart}?&music_info=1&description=1"
    allow="fullscreen">
  </iframe>
{:else if embedType === 'vimeo'}
  <iframe
    title="embed"
    style="width: 500px; height: 281px"
    width="500"
    height="380"
    src="https://player.vimeo.com/video/{relevantPart}?title=0&byline=0"
    allow="fullscreen">
  </iframe>
{/if}