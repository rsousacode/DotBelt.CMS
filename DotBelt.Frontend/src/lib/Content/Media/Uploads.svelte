<script lang="ts">
  import {onMount} from "svelte";
  import {setDashboardData, updateDashboardFragment} from "$lib/Dashboard/DashboardStore.svelte";
  import MediaPopup from "$lib/Content/Media/MediaPopup.svelte";
  import type {Maybe} from "yup";
  import type {
    DotBeltMutation,
    ThumbnailResponse, UploadResponse,
    UploadsConnection
  } from '$lib/API/GraphQL/generated';
  import type {PaginationQuery} from "$lib/API/GraphQL/PaginationQuery";
  import {getUploads} from "$lib/Content/Media/GetUploads";
  import DropfileZone from "$lib/Content/Media/DropfileZone.svelte";
  import {type FetchPolicy, gql} from "@apollo/client/core/index.js";
  import CheckMarkUnselected from "$lib/Utilities/Icons/CheckMarkUnselected.svelte";
  import CheckMarkSelected from "$lib/Utilities/Icons/CheckMarkSelected.svelte";
  import CloseIcon from "$lib/Utilities/Icons/CloseIcon.svelte";
  import ReusablePopup from "$lib/Utilities/Modal/BoilerplateModal.svelte";
  import BoilerplateModal from "$lib/Utilities/Modal/BoilerplateModal.svelte";
  import {ModalType} from "$lib/Utilities/Modal/ModalType";
  import {apolloClientStore} from "$lib/API/GraphQL/apolloClientStore";
  import type { UploadsContext } from '$lib/Content/Media/UploadsContext';

  let uploadsResult: Maybe<UploadsConnection> = $state();

  let thumbnails: Maybe<Array<ThumbnailResponse>> = $state();

  let postsPerPage = $state(50);

  let uploadOverlayOpen: boolean = $state(false);

  type UploadsMode = "default" | "selection";


  let mode: UploadsMode = $state("default");

  let selectedImages: Maybe<Array<number>> = $state();

  let mediaPopup :  MediaPopup;

  let confirmDeletionModal: BoilerplateModal;

  let {context = 'Page'} : {context: UploadsContext} = $props();

  let popupOpen: boolean = $state(false);


  onMount(async () => {

    if (context === 'Page') {
      setDashboardData({title: "Media", subtitle: "Manage your media here"})
      updateDashboardFragment(uploadButton);
      await fetchAndReset();
    }

  });


  export async function open(imageSelectedCallback: (upload: UploadResponse) => void,
                             imageSelectedId: Maybe<number> | undefined = undefined) {
    await fetchAndReset();

    selectedImages = [];

    if(imageSelectedId) {
      selectedImages.push(imageSelectedId);
    }

    function onImageSelected(upload: UploadResponse) {
      closePopup();
      imageSelectedCallback(upload);
    }

    mediaPopup.assignOneImageSelectedCallback(onImageSelected);
    popupOpen = true;

  }

  async function fetchAndReset(fetchPolicy: FetchPolicy = 'cache-first') {

    await fetchUploads({
      first: postsPerPage,
      last: undefined,
      before: undefined,
      after: undefined,
    }, fetchPolicy);

    resetData();

  }

  async function fetchUploads(variables: PaginationQuery, fetchPolicy: FetchPolicy = 'cache-first') {
    const apolloClient = apolloClientStore.getClient();

    if (!apolloClient) {
      console.error("API Client not available");
      return;
    }

    uploadsResult = await getUploads(apolloClient, variables, fetchPolicy);
  }

  function onUploadFilesClicked() {
    uploadOverlayOpen = true;
  }

  async function onUploadSuccess() {
    await fetchAndReset("network-only");
  }

  function onClickSelectionMode() {
    mode = "selection";
  }

  function resetData() {
    if (uploadsResult?.nodes && uploadsResult.nodes.length) {
      thumbnails = [...uploadsResult.nodes];
    } else {
      thumbnails = [];
    }
  }

  async function onLoadMoreClicked() {
    if (!uploadsResult) {
      return;
    }

    if (!uploadsResult?.pageInfo.hasNextPage) {
      return;
    }
    await fetchUploads({
      first: postsPerPage,
      last: undefined,
      before: undefined,
      after: uploadsResult.pageInfo.endCursor,
    });

    if (uploadsResult?.nodes && uploadsResult.nodes.length) {
      if (thumbnails?.length) {
        thumbnails = [...thumbnails, ...uploadsResult.nodes];
      }
    }
  }

  function onClickImageDefaultMode(image: ThumbnailResponse) {
    if (!thumbnails || !image.uploadId) {
      return;
    }
    mediaPopup.openMediaPopup(image.uploadId, thumbnails);

  }

  function onClickImageSelectionMode(image: ThumbnailResponse) {
    if(!image || !image.uploadId) return;

    if (selectedImages) {
      if (selectedImages.includes(image.uploadId)) {
        selectedImages = selectedImages.filter(x => x !== image.uploadId);
      } else {
        selectedImages = [...selectedImages, image.uploadId]
      }
    } else {
      selectedImages = [image.uploadId]
    }
  }

  function onClickImage(image: ThumbnailResponse) {
    if (mode === 'default') {
      onClickImageDefaultMode(image);
    }
    if (mode === 'selection') {
      onClickImageSelectionMode(image);
    }
  }


  function imageIsSelected(imageId: Maybe<number> | undefined) {
    return selectedImages?.length && imageId && selectedImages.includes(imageId);
  }

  async function deleteFiles() {

    if (!selectedImages) {
      console.error("there's nothing to delete");
      return;
    }

    const client = apolloClientStore.getClient();

    if (client === null) {
      return;
    }

    const mutation = gql`
  mutation deleteFiles($uploadIds: [Int!]!) {
      deleteUploads(input: {uploadIds: $uploadIds}) {
          removeUploadsResult {
              deletedIds
          }
      }
  }
`;
    const {errors} = await client.mutate<DotBeltMutation>({
      mutation: mutation,
      variables: {
        uploadIds: selectedImages
      }
    });

    if(!errors) {
      await fetchAndReset('no-cache');
      selectedImages = [];
    }

  }


  function onDeleteFilesClicked() {
    confirmDeletionModal.openModal();
  }

  function closePopup() {
    popupOpen = false;
  }

  function getImageItemCss(upload: ThumbnailResponse) {

    let cssClasses = "";

    if (mode === 'selection') {
      cssClasses += 'image-selection-mode faded';
    }

    if(context !== 'Page') {
      cssClasses += 'image-selection-mode';
    }

    if (imageIsSelected(upload.uploadId)) {
      cssClasses += ` selected`;
    }

    return cssClasses;
  }

</script>

<MediaPopup context={context} bind:this={mediaPopup}/>



{#if context === 'Page' || popupOpen}
  {#if context !== 'Page'}
    <div class="overlay"></div>
  {/if}

  <div class={context === 'Page' ? '' : 'modal-container'}>
  {#if context !== 'Page'}
    <div class="modal-header">
      <div class="chevron-buttons">
        {@render uploadButton()}
      </div>

      <span class="text-white modal-header-title"></span>

      <button class="dashboard-icon" onclick={closePopup}>
        <CloseIcon/>
      </button>

    </div>
  {/if}

  <div class="media-section">
    <div class="media-images-container">
      {#if thumbnails && thumbnails.length}
        {#each thumbnails as upload}
          <div
            class="image-item {getImageItemCss(upload)}"
            onclick={() => onClickImage(upload)}>
            {#if mode === 'selection'}
              <div class="check-mark-selection-mode">
                {#if imageIsSelected(upload.id)}
                  <CheckMarkSelected/>
                {:else}
                  <CheckMarkUnselected/>
                {/if}
              </div>
            {/if}
            <img src={`/${upload.relativeUrl}`}/>
          </div>
        {/each}
      {:else}
        <p>No media found</p>
      {/if}
    </div>
    <div class="load-more-section">
      <button disabled={!uploadsResult?.pageInfo.hasNextPage} class="btn btn-secondary" onclick={onLoadMoreClicked}>
        Load more
      </button>
    </div>
  </div>
</div>
{/if}

{#snippet uploadButton()}
  {#if mode === 'default'}
    <button class="btn btn-primary" type="button" onclick={onUploadFilesClicked}>
      Upload files
    </button>

    {#if context === 'Page'}
      <button style="margin-left: 20px" class="btn btn-secondary" type="button" onclick={onClickSelectionMode}>
        Select files
      </button>
    {/if}
  {/if}

  {#if mode === 'selection'}
    <button disabled={!(selectedImages?.length && selectedImages.length > 0)} style="margin-left: 20px" class="btn btn-danger" type="button" onclick={onDeleteFilesClicked}>
      Delete files
    </button>
    <button style="margin-left: 20px" class="icon-button" type="button" onclick={() => mode = 'default'}>
      <CloseIcon/>
    </button>
  {/if}


{/snippet}

{#if mode === 'default'}
  <DropfileZone
    onUploadSuccess={onUploadSuccess}
    onUploadError={() => console.error('error uploading image')}
    bind:uploadOverlayOpen={uploadOverlayOpen}
  />
{/if}

<ReusablePopup modalType={ModalType.Action}
               bind:this={confirmDeletionModal}
               onModalConfirm={deleteFiles}>

  <p>Are you sure you want to remove {selectedImages?.length} image(s)?</p>
</ReusablePopup>

<style>

    .image-selection-mode-faded > img {
        filter: contrast(0.5);
    }

    .image-selection-mode.faded > img {
        filter: contrast(0.5);
    }

    .check-mark-selection-mode {
        position: relative;
        font-size: 28px;
        margin-left: 10px;
        margin-bottom: -45px;
        z-index: 1;
        color: var(--bs-white);
    }

    .image-selection-mode.selected {
        border: 4px solid var(--bs-warning);
        contain: strict;
    }

    .load-more-section {
        margin-bottom: 2rem;
        margin-top: 2rem;
    }

    .media-images-container {
        display: flex;
        flex-wrap: wrap;
        align-self: center;
        justify-content: flex-start;
        width: 100%;
    }

    .image-item img,
    .image-item {
        width: 139px;
        height: 139px;
    }

    .image-item img {
        object-fit: cover;
        max-width: 100%;
    }

    .image-item {
        margin: 10px;
        cursor: pointer;
    }

    .media-section {
        display: flex;
        align-items: center;
        justify-content: flex-start;
        flex-direction: column;
    }
</style>