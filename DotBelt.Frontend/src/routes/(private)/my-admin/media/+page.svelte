<script lang="ts">
    import DashboardContainer from "$lib/admin/Dashboard/DashboardContainer.svelte";
    import {onMount} from "svelte";
    import {setDashboardData} from "$lib/admin/Dashboard/DashboardStore.svelte";
    import MediaPopup from "$lib/admin/Media/MediaPopup.svelte";

    type imageMedia = {url: string};

    let images : imageMedia[] = $state([]);

    let imageModalOpen = $state(false);

    let selectedImage : imageMedia | undefined = $state(undefined)

    function onClickImage(image: imageMedia) {
        selectedImage = image;
        imageModalOpen = true;
    }

    onMount(() => {
        setDashboardData({title: "Media", subtitle: "Manage your media here"})

        for (let i = 0; i < 100; i++) {
            images.push({url: "/images/placeholder-image.png"})
        }
    })
</script>

{#if imageModalOpen}
  <MediaPopup image={selectedImage} onClose={() => imageModalOpen = false}/>
{/if}

<DashboardContainer>
  <div class="media-section">
    <div class="media-images-container">

      {#each images as image}
        <div class="image-item" onclick={() => onClickImage(image)}>
          <img src={image.url} />
        </div>
      {/each}

    </div>
  </div>
</DashboardContainer>

<style>
  .media-images-container {
      display: flex;
      flex-wrap: wrap;
      align-self: center;
      justify-content: center;
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