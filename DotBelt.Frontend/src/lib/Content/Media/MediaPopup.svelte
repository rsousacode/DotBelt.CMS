<script lang="ts">
  import {onDestroy, onMount} from "svelte";
  import convertSize from "convert-size";
  import CloseIcon from "$lib/Utilities/Icons/CloseIcon.svelte";
  import ChevronLeft from "$lib/Utilities/Icons/ChevronLeft.svelte";
  import ChevronRight from "$lib/Utilities/Icons/ChevronRight.svelte";
  import {gql} from "@apollo/client/core/index.js";
  import {apolloClientStore} from "$lib/API/GraphQL/apolloClientStore";
  import type {DotBeltQuery, UploadResponse} from "$lib/API/GraphQL/generated";

  let body: Element;


  let popupOpen : boolean = $state(false);

  let loading = $state(false);

  let upload : UploadResponse | undefined = $state();
  let uploads: UploadResponse[] | undefined = $state();

  let metaData : Record<string, unknown> | undefined = $state();

  let previousElementId : number | undefined = $state();
  let nextElementId : number | undefined = $state();

  function onClose() {
    upload = undefined;
    popupOpen = false;
    loading = false;
  }

  export async function openMediaPopup(imageId: number, uploads: UploadResponse[] | undefined = undefined) {
    popupOpen = false;
    loading = true;
    uploads = uploads;

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

    loading = false;
    popupOpen = true;
    previousElementId = previousElementId;
    nextElementId = nextElementId;

    if(upload && upload.metaData != null) {
      metaData = JSON.parse(upload.metaData);
      console.log(metaData);
    }
    //setOverflowHidden();
  }

  function closeModal() {
    unsetOverflowHidden();
  }

  onDestroy(() => {
    //unsetOverflowHidden();
  })

  function setOverflowHidden() {
    body.classList.add("overflow-hidden");
  }

  function unsetOverflowHidden() {
    body.classList.remove("overflow-hidden");

  }

</script>

<svelte:body bind:this={body}/>
{#if popupOpen && upload}
  <p>{upload.metaData}</p>
  <div class="overlay"></div>
  <div class="modal-container">
      <div class="modal-header">
        <div class="chevron-buttons">
          <button style="font-size: 24px;" class="dashboard-icon" onclick={onClose}>
            <ChevronLeft/>
          </button>
          <button style="font-size: 24px;" class="dashboard-icon" onclick={onClose}>
            <ChevronRight/>
          </button>
        </div>
        <span class="text-white modal-header-title"></span>

        <button class="dashboard-icon" onclick={onClose}>
          <CloseIcon/>
        </button>

      </div>
      <div class="modal-content">
        <div class="media-image-container">
          <img src={`/${upload.relativeUrl}`} alt="">
        </div>
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
            <div class="form-group">
              <label for="title">Title</label>
              <input type="email" class="form-control" id="title">
            </div>
            <div class="form-group">
              <label for="caption">Caption</label>
              <textarea class="form-control" id="caption" rows="3"></textarea>
            </div>
            <div class="form-group">
              <label for="alt-text">Alt Text</label>
              <input type="email" class="form-control" id="alt-text">
            </div>
            <div class="form-group">
              <label for="description">Description</label>
              <textarea class="form-control" id="description" rows="3"></textarea>
            </div>

          </form>
        </div>
      </div>
  </div>
{/if}
