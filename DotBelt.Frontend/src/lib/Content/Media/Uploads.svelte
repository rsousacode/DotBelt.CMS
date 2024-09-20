<script lang="ts">
  import DashboardContainer from "$lib/Dashboard/DashboardContainer.svelte";
  import {onMount} from "svelte";
  import {setDashboardData} from "$lib/Dashboard/DashboardStore.svelte";
  import MediaPopup from "$lib/Content/Media/MediaPopup.svelte";
  import type {Maybe} from "yup";
  import type {UploadResponse, UploadsConnection} from "$lib/API/GraphQL/generated";
  import ApolloClientProvider from "$lib/API/GraphQL/ApolloClientProvider.svelte";
  import type {PaginationQuery} from "$lib/API/GraphQL/PaginationQuery";
  import {getUploads} from "$lib/Content/Media/GetUploads";
  import DropfileZone from "$lib/Content/Media/DropfileZone.svelte";

  let imageModalOpen = $state(false);

  let selectedImage: UploadResponse | undefined = $state(undefined)

  let uploadsResult: Maybe<UploadsConnection> | undefined = $state(undefined);

  let apolloClientProvider: ApolloClientProvider;

  let postsPerPage = $state(10);


  async function fetchUploads(variables: PaginationQuery) {
    const apolloClient = apolloClientProvider.GetApolloSvelteClient();
    if (!apolloClient) {
      console.error("API Client not available");
      return;
    }
    uploadsResult = await getUploads(apolloClient, variables);
  }


  function onClickImage(image: UploadResponse) {
    selectedImage = image;
    imageModalOpen = true;
  }

  onMount(async () => {
    setDashboardData({title: "Media", subtitle: "Manage your media here"})
    await fetchUploads({
      first: postsPerPage,
      last: undefined,
      before: undefined,
      after: undefined,
    });
  })
</script>

{#if imageModalOpen}
  <MediaPopup image={selectedImage} onClose={() => imageModalOpen = false}/>
{/if}

<DashboardContainer>
  <ApolloClientProvider bind:this={apolloClientProvider}>


    <div class="media-section">
      <div class="media-images-container">


        {#if !!uploadsResult && uploadsResult.nodes?.length}
          {#each uploadsResult.nodes as upload}
            <div class="image-item" onclick={() => onClickImage(upload)}>
              <img src={`/${upload.relativeUrl}`}/>
            </div>
          {/each}
        {:else}
          <p>No media found</p>
        {/if}

      </div>
    </div>
  </ApolloClientProvider>
</DashboardContainer>
<DropfileZone/>

<style>
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
    }

    .image-item {
        margin: 10px;
        cursor: pointer;
    }

    .media-section {
        display: flex;
        align-items: center;
        justify-content: center;
    }
</style>