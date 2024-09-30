<script lang="ts">
  import convertSize from "convert-size";
  import CloseIcon from "$lib/Utilities/Icons/CloseIcon.svelte";
  import ChevronLeft from "$lib/Utilities/Icons/ChevronLeft.svelte";
  import ChevronRight from "$lib/Utilities/Icons/ChevronRight.svelte";
  import {gql} from "@apollo/client/core/index.js";
  import {apolloClientStore} from "$lib/API/GraphQL/apolloClientStore";
  import type {DotBeltQuery, ThumbnailResponse, UploadResponse} from "$lib/API/GraphQL/generated";
  import type { UploadsContext } from '$lib/Content/Media/UploadsContext';


  let popupOpen : boolean = $state(false);

  let upload : UploadResponse | undefined = $state();
  let thumbnails: ThumbnailResponse[] | undefined = $state();

  let metaData : Record<string, unknown> | undefined = $state();

  let imageElement : HTMLImageElement | undefined = $state();

  type UploadSelectedCallback = (upload: UploadResponse) => void;

  type Props = {context: UploadsContext, onOneImageSelected: UploadSelectedCallback | undefined}

  let {context } : Props = $props();

  let onOneImageSelected : UploadSelectedCallback | undefined = $state();


  function closePopup() {
    upload = undefined;
    popupOpen = false;
  }

  export async function openMediaPopup(imageId: number, uploads: ThumbnailResponse[] | undefined = undefined) {
    thumbnails = uploads;
    await loadImage(imageId);
    //setOverflowHidden();
  }

  export function assignOneImageSelectedCallback(imageSelectedCallback: (upload: UploadResponse) => void) {
    onOneImageSelected = imageSelectedCallback;
  }

  async function loadImage(imageId: number) {
    const query = gql`
      query getUpload ($id: Int!) {
          uploadById(id: $id) {
             id,
             mimeType,
             description,
             title,
             metaData,
             relativeUrl,
             fileName
             publishDate,
             author {
                userName
             }

          }
      }
    `;

    const client = apolloClientStore.getClient();
    const {data: {uploadById}} = await client.query<DotBeltQuery>({
      query: query,
      variables: {id: imageId}
    })

    upload = uploadById[0];

    if(upload && upload.metaData != null) {
      metaData = JSON.parse(upload.metaData);
    }

    if (imageElement) {
      imageElement.src = `/${upload.relativeUrl}`
      await imageElement.decode();
    }

    popupOpen = true;
  }

  const getNextThumbnail = (array: ThumbnailResponse[], id: number ): ThumbnailResponse | undefined => {
    const index = array.findIndex(el => el.uploadId === id);
    if (index !== -1 && index < array.length - 1) {
      return array[index + 1];
    }
    return undefined;
  }

  const getPreviousThumbnail = (array: ThumbnailResponse[], id: number ): ThumbnailResponse | undefined => {
    const index = array.findIndex(el => el.uploadId === id);
    if (index > 0) {
      return array[index - 1];
    }
    return undefined;
  }

  function onImageSelected() {
    if(onOneImageSelected !== undefined && upload) {
      onOneImageSelected(upload);
      closePopup();
    }
  }

  async function onPreviousClicked() {
    if (!upload || !thumbnails) return;
    const previousElement = getPreviousThumbnail(thumbnails, upload.id);
    if(!previousElement) return;
    await loadImage(previousElement.id);
  }

  async function onNextClicked() {
    if (!upload || !thumbnails) return;
    const nextElement = getNextThumbnail(thumbnails, upload.id);
    if(!nextElement) return;
    await loadImage(nextElement.id);
  }

  function onKeyUp(e: KeyboardEvent) {
    const key = e.key;

    if(key === 'ArrowLeft') {
      onPreviousClicked();
    }

    if(key === 'ArrowRight') {
      onNextClicked();
    }

    if(key === 'Escape') {
      closePopup()
    }

  }


</script>
<svelte:body onkeyup={(e) => popupOpen ? onKeyUp(e) : {}}  />

<div class={(popupOpen && upload) ? '' : 'hide' }>

  <div class="overlay"></div>
  <div class="modal-container media-popup" >
      <div class="modal-header">
        {#if upload && thumbnails}
          <div class="chevron-buttons">
            <button disabled={!thumbnails || !getPreviousThumbnail(thumbnails, upload.id)} style="font-size: 24px;" class="dashboard-icon" onclick={onPreviousClicked}>
              <ChevronLeft/>
            </button>
            <button disabled={!thumbnails || !getNextThumbnail(thumbnails, upload.id)} style="font-size: 24px;" class="dashboard-icon" onclick={onNextClicked}>
              <ChevronRight/>
            </button>
          </div>
        {/if}

        <span class="text-white modal-header-title"></span>

        <button class="dashboard-icon" onclick={closePopup}>
          <CloseIcon/>
        </button>

      </div>
      <div class="modal-content upload">
        <div class="upload-information">
          <div class="media-image-container">
            <img bind:this={imageElement} src="/images/placeholder-image.png" alt="">
          </div>
          {#if upload}
            <div class="media-form-container">
              <div class="image-meta-data">
                <div class="meta-data-item">
                  <span class="meta-data-title">Uploaded on: </span>
                  <span>{new Date(upload.publishDate).toLocaleString()}</span>
                </div>

                <div class="meta-data-item">
                  <span class="meta-data-title">Uploaded by: </span>
                  <span>{upload.author?.userName}</span>
                </div>

                <div class="meta-data-item">
                  <span class="meta-data-title">Filename: </span>
                  <span>{upload.fileName}</span>
                </div>

                <div class="meta-data-item">
                  <span class="meta-data-title">File type: </span>
                  <span>{upload.mimeType}</span>
                </div>
                {#if metaData}
                  <div class="meta-data-item">
                    <span class="meta-data-title">File size: </span>
                    <span>{convertSize(typeof metaData["Size"] === 'number' ? metaData["Size"] : 0)}</span>
                  </div>
                  <div class="meta-data-item">
                    <span class="meta-data-title">Dimensions: </span>
                    <span>{metaData["Width"]} by {metaData["Height"]} pixels</span>
                  </div>
                {/if}
              </div>
              <form>
                <div class="form-group mb-3">
                  <label for="title">Title</label>
                  <input type="email" class="form-control" id="title">
                </div>
                <div class="form-group mb-3">
                  <label for="caption">Caption</label>
                  <textarea class="form-control" id="caption" rows="3"></textarea>
                </div>
                <div class="form-group mb-3">
                  <label for="alt-text">Alt Text</label>
                  <input type="email" class="form-control" id="alt-text">
                </div>
                <div class="form-group mb-3">
                  <label for="description">Description</label>
                  <textarea class="form-control" id="description" rows="3"></textarea>
                </div>

              </form>
            </div>
          {/if}
        </div>

        <div class="actions-buttons">
          <button class="btn btn-primary" onclick={onImageSelected}>Select</button>
        </div>
      </div>
  </div>

</div>
<style>
  .hide {
      display: none;
  }

  .modal-content.upload {
      flex-direction: column;
  }

  .modal-container.media-popup {
      z-index: 3;
  }

  .upload-information {
      display: flex;
      width: 100%;
      justify-content: space-evenly;
  }

  .actions-buttons {
      display: flex;
      width: 100%;
      justify-content: end;
  }

</style>