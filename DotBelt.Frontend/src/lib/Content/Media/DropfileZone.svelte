<script lang="ts">
  import CloseIcon from "$lib/Utilities/Icons/CloseIcon.svelte";

  type Props = { onUploadSuccess: () => void, onUploadError: () => void, uploadOverlayOpen: boolean };

  let {onUploadSuccess, onUploadError , uploadOverlayOpen = $bindable<boolean>(false)}: Props = $props();

  let filesInArea = $state<boolean>(false);

  let inputFilesElement : Element;

  function onCloseUploadOverlay() {
    uploadOverlayOpen = false;
  }

  function onDragEnter(e: DragEvent) {
    e.preventDefault();
    if (e.dataTransfer === null) return;

    uploadOverlayOpen = true;
    filesInArea = true;
    //dropZone.classList.add(hoverClassName);
  }

  function onDragover(e: DragEvent) {
    e.preventDefault();
    //dropZone.classList.add(hoverClassName);
  }

  function onDragleave(e: DragEvent) {
    e.preventDefault();
    filesInArea = false;
    //dropZone.classList.remove(hoverClassName);
  }

  function handleUploads(files) {
    if (files.length > 0) {
      const data = new FormData();
      for (const file of files) {
        data.append('files', file);
      }


      fetch('/api/uploads', {
        method: 'POST',
        body: data

      }).then(async (data) => {
        uploadOverlayOpen = false;
        onUploadSuccess();
      })
        .catch(() => {
          onUploadError();
        });
    }
  }

  function onDrop(e: DragEvent) {
    e.preventDefault();

    filesInArea = false;

    if (e.dataTransfer === null) return;

    const files = Array.from(e.dataTransfer.files);

    handleUploads(files);
  }

  function onInputFilesChange(e) {
    const files = e.target.files;
    handleUploads(files);
  }

  function onKeyUp(e: KeyboardEvent) {
    const key = e.key;

    if(key === 'Escape') {
      onCloseUploadOverlay();
    }
  }


</script>

{#if uploadOverlayOpen}
  <div class="upload-overlay {filesInArea && uploadOverlayOpen ? 'drag-enter' : ''}">
    <div class="upload-overlay-header">
      <button  class="icon-button" onclick={onCloseUploadOverlay}>
        <CloseIcon/>
      </button>
    </div>
    <div class="upload-overlay-frame">
      <div class="upload-overlay-title">
        Drag files to upload
      </div>
      <p>or</p>
      <button class="btn btn-primary" onclick={() => inputFilesElement.click()}>Select files</button>
      <input style="position: absolute; top: -500px;"  onchange={onInputFilesChange} hidden bind:this={inputFilesElement}  type="file" multiple name="uploads">

    </div>

  </div>
{/if}


<svelte:body onkeyup={onKeyUp}
             ondragover={onDragover}
             ondrop={onDrop}
             ondragenter={onDragEnter}
             ondragleave={onDragleave}
/>

<style>
    .upload-overlay-header {
        font-size: 52px;
        position: absolute;
        right: 69px;
        top: 20px;
    }

    .upload-overlay-frame {
        justify-content: center;
        align-items: center;
        display: flex;
        flex-direction: column;
        height: 100%;
    }

    .upload-overlay {
        position: absolute;
        top: 0;
        background: #000000b8;
        width: 100%;
        height: 100%;
        display: flex;
        flex-direction: column;
        z-index: 2;
    }

    .upload-overlay.drag-enter {
        border: 2px solid var(--bs-primary);
        cursor: copy;

    }

    .upload-overlay-title {
        font-size: 20px;
    }
</style>