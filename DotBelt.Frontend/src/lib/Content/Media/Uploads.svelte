<script lang="ts">
  import DashboardContainer from "$lib/Dashboard/DashboardContainer.svelte";
  import {onMount} from "svelte";
  import {setDashboardData, updateDashboardFragment} from "$lib/Dashboard/DashboardStore.svelte";
  import MediaPopup from "$lib/Content/Media/MediaPopup.svelte";
  import type {Maybe} from "yup";
  import type {UploadResponse, UploadsConnection} from "$lib/API/GraphQL/generated";
  import ApolloClientProvider from "$lib/API/GraphQL/ApolloClientProvider.svelte";
  import type {PaginationQuery} from "$lib/API/GraphQL/PaginationQuery";
  import {getUploads} from "$lib/Content/Media/GetUploads";
  import DropfileZone from "$lib/Content/Media/DropfileZone.svelte";
  import type {FetchPolicy} from "@apollo/client/core/index.js";
  import CheckMarkUnselected from "$lib/Utilities/Icons/CheckMarkUnselected.svelte";
  import CheckMarkSelected from "$lib/Utilities/Icons/CheckMarkSelected.svelte";
  import CloseIcon from "$lib/Utilities/Icons/CloseIcon.svelte";
  import ReusablePopup from "$lib/Utilities/Popup/ReusablePopup.svelte";

  let imageModalOpen = $state(false);

  let selectedImage: UploadResponse | undefined = $state(undefined)

  let uploadsResult: Maybe<UploadsConnection> = $state();

  let uploads: Maybe<Array<UploadResponse>> = $state();

  let apolloClientProvider: ApolloClientProvider;

  let postsPerPage = $state(50);

  let uploadOverlayOpen: boolean = $state(false);

  type UploadsMode = "default" | "selection";

  let mode: UploadsMode = $state("selection");

  let selectedImages: Maybe<Array<number>> = $state();


  onMount(async () => {
    setDashboardData({title: "Media", subtitle: "Manage your media here"})
    updateDashboardFragment(uploadButton);

    await fetchUploads({
      first: postsPerPage,
      last: undefined,
      before: undefined,
      after: undefined,
    });

    resetData();

  })

  async function fetchUploads(variables: PaginationQuery, fetchPolicy: FetchPolicy = 'cache-first') {
    const apolloClient = apolloClientProvider.GetApolloSvelteClient();
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
    await fetchUploads({
      first: postsPerPage,
      last: undefined,
      before: undefined,
      after: undefined,
    }, 'network-only');

    resetData();
  }

  function onClickSelectionMode() {
    mode = "selection";
  }

  function resetData() {
    if (uploadsResult?.nodes && uploadsResult.nodes.length) {
      uploads = [...uploadsResult.nodes];
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

  function onClickImageDefaultMode(image: UploadResponse) {
    selectedImage = image;
    imageModalOpen = true;
  }

  function onClickImageSelectionMode(image: UploadResponse) {
    if(selectedImages) {
      if(selectedImages.includes(image.id)) {
        selectedImages = selectedImages.filter(x => x !== image.id);
      } else {
        selectedImages = [...selectedImages, image.id]
      }
    } else {
      selectedImages = [image.id]
    }
  }

  function onClickImage(image: UploadResponse) {
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


  function onDeleteFilesClicked() {

  }

</script>

{#if imageModalOpen}
  <MediaPopup image={selectedImage} onClose={() => imageModalOpen = false}/>
{/if}

<DashboardContainer>
  <ApolloClientProvider bind:this={apolloClientProvider}>
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

  </ApolloClientProvider>
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

    <button style="margin-left: 20px" class="btn btn-danger" type="button" onclick={onClickSelectionMode}>
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
    onUploadError={() => console.log('error uploading image')}
    bind:uploadOverlayOpen={uploadOverlayOpen}
  />
{/if}

<ReusablePopup showPopup={true}>
  <p>You sure?</p>
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