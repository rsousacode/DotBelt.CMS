<script lang="ts">
  import type { EmbedSvelteProps, SupportedEmbed } from '$lib/Content/EditorJS/Blocks/embed/EmbedProps';
  import { onMount } from 'svelte';

  const youtubeRegex = /(?:https?:\/\/)?(?:www\.)?(?:(?:youtu\.be\/)|(?:youtube\.com)\/(?:v\/|u\/\w\/|embed\/|watch))(?:(?:\?v=)?([^#&?=]*))?((?:[?&]\w*=[\w%+]*))*/;

  const tiktokRegex = /https:\/\/(?:www|m)?\.?(?:tiktok\.com)\/(?:@[\w.-]+\/video\/|embed\/|h5\/share\/usr\/|v\/|share\/user\/|trending\?shareId=)?(\d+)/;

  let {url, onPropsChange} : EmbedSvelteProps = $props();

  let embedType : SupportedEmbed | undefined = $state();

  let relevantPart : string | undefined = $state();

  onMount(() => {
    detectEmbedType(url);
    updateRelevantPart();
  })

  function detectEmbedType(url: string | undefined) {
    if(!url) return;

    url = url.toLowerCase();

    if (url.includes("youtube.com") || url.includes("youtu.be")) {
      embedType = "youtube";
    }

    if (url.includes("tiktok")) {
      embedType = "tiktok";
    }

    console.log('detectEmbedType', embedType);

  }

  // Function to extract video ID
  function extractVideoId(url: string) {

    let usedRegex : RegExp = youtubeRegex;

    if(embedType === 'tiktok') {
      usedRegex = tiktokRegex;
    }

    const match = url.match(usedRegex);


    if (match) {
      return match[1];
    }

    return undefined; // No match
  }

  function onUrlChange(e) {
    if(typeof e.currentTarget.value !== 'string') return;
    url = e.currentTarget.value;
    updateRelevantPart();
  }

  function updateRelevantPart() {
    detectEmbedType(url);
    if(url) {
      relevantPart = extractVideoId(url);

      console.log('updateRelevantPart', relevantPart);

      onPropsChange({ url: url, embedType: embedType});
    }
  }

  function onButtonMouseHover(e) {
    e.stopImmediatePropagation();
    showEditButton = true;
  }

  function onFrameContainerMouseOver() {
    showEditButton = true
  }

  function onFrameContainerMouseOut(e) {
    if (e.relatedTarget === editButtonElement) return;
    showEditButton = false
  }

  let editButtonElement : HTMLElement | undefined = $state();

  let showEditButton : boolean = $state(false);
  let editMode : boolean = $state(false);

</script>


{#if relevantPart && !editMode}
  <div class="embed-container" onmouseover={onFrameContainerMouseOver} onmouseout={onFrameContainerMouseOut}>
    {#if showEditButton}
      <button bind:this={editButtonElement} onclick={() => editMode = true} onmouseover={onButtonMouseHover} type="button" class="btn btn-primary embed-edit-button">Edit</button>
    {/if}
    {#if embedType === 'youtube'}
      <iframe
        class={relevantPart ? "" : "hidden"}
        title=""
        width="500"
        height="380"
        src="https://www.youtube.com/embed/{relevantPart}"
        frameborder="0"
        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
        referrerpolicy="strict-origin-when-cross-origin" allowfullscreen>

      </iframe>
      {:else if embedType === 'tiktok'}
        <iframe height="300" width= "400" src="https://www.tiktok.com/player/v1/{relevantPart}?&music_info=1&description=1" allow="fullscreen" title="test"></iframe>
      {/if}
  </div>
{/if}

<div class="embed-frame {(relevantPart && !editMode) ? 'hidden' : ''}">
  <div class="form-group embed-url-input">
    <label for="">Insert the URL</label>
    <div class="form-group d-flex">
      <input value={url} class="form-control mx-2"  onchange={onUrlChange} type="text">
      {#if editMode}
        <button class="btn btn-primary " type="button" onclick={() => editMode = false}>Apply</button>
      {/if}
    </div>

  </div>
</div>
<style>

  .embed-edit-button {
    margin-left: 20px;
    margin-top: 20px;
    position: absolute;
  }
  .hidden  {
    display: none !important;
  }
  .embed-url-input {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
  .embed-frame {
    height: 380px;
    width: 500px;
    background: #00000021;
    display: flex;
    justify-content: center;
    align-items: center;
  }
</style>