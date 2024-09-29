<script lang="ts">
  import DashboardContainer from "$lib/Dashboard/DashboardContainer.svelte";
  import {onMount} from "svelte";
  import {setDashboardData, updateDashboardFragment} from "$lib/Dashboard/DashboardStore.svelte";
  import MediaPopup from "$lib/Content/Media/MediaPopup.svelte";
  import type {Maybe} from "yup";
  import type {
    DotBeltMutation,
    ThumbnailResponse,
    UploadsConnection
  } from "$lib/API/GraphQL/generated";
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

  let uploadsResult: Maybe<UploadsConnection> = $state();

  let uploads: Maybe<Array<ThumbnailResponse>> = $state();

  let postsPerPage = $state(50);

  let uploadOverlayOpen: boolean = $state(false);

  type UploadsMode = "default" | "selection";

  type UploadsContext = 'Page' | 'OneMediaSelection' | 'MultipleMediaSelection';

  let mode: UploadsMode = $state("default");

  let selectedImages: Maybe<Array<number>> = $state();

  let mediaPopup : MediaPopup;

  let confirmDeletionModal: BoilerplateModal;

  let {context = 'Page'} : {context: UploadsContext} = $props();


  onMount(async () => {

    if (context === 'Page') {
      setDashboardData({title: "Media", subtitle: "Manage your media here"})
      updateDashboardFragment(uploadButton);
    }

    await fetchAndReset();
  });

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
      uploads = [...uploadsResult.nodes];
    } else {
      uploads = [];
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
      if (uploads?.length) {
        uploads = [...uploads, ...uploadsResult.nodes];
      }
    }
  }

  function onClickImageDefaultMode(image: ThumbnailResponse) {
    mediaPopup.openMediaPopup(image.uploadId);
  }

  function onClickImageSelectionMode(image: ThumbnailResponse) {
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


  function imageIsSelected(imageId: number) {
    return selectedImages?.length && selectedImages.includes(imageId);
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

</script>

<MediaPopup bind:this={mediaPopup}/>

<DashboardContainer>
  <div class="media-section">
    <div class="media-images-container">
      {#if uploads && uploads.length}
        {#each uploads as upload}
          <div
            class="image-item {mode === 'selection' ? 'image-selection-mode' : ''}  {imageIsSelected(upload.id) ? 'selected' : ''}"
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

</DashboardContainer>

{#snippet uploadButton()}
  {#if mode === 'default'}
    <button class="btn btn-primary" type="button" onclick={onUploadFilesClicked}>
      Upload files
    </button>

    <button style="margin-left: 20px" class="btn btn-secondary" type="button" onclick={onClickSelectionMode}>
      Select files
    </button>
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

    .image-selection-mode > img {
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