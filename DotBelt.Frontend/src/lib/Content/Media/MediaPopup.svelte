<script lang="ts">
  import {onMount} from "svelte";
  import {beforeNavigate} from "$app/navigation";
  import CloseIcon from "$lib/Utilities/Icons/CloseIcon.svelte";
  import ChevronLeft from "$lib/Utilities/Icons/ChevronLeft.svelte";
  import ChevronRight from "$lib/Utilities/Icons/ChevronRight.svelte";

  let body: Element;

  let {onClose, image}: { onClose: () => void, image: any } = $props();

  onMount(async () => {
    body.classList.add("overflow-hidden");
  });

  beforeNavigate(() => {
    body.classList.remove("overflow-hidden");
  });

</script>

<svelte:body bind:this={body}/>
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
        <img src={`/${image.relativeUrl}`} alt="">
      </div>
      <div class="media-form-container">
        <div class="image-meta-data">
          <div class="meta-data-item">
            <span class="meta-data-title">Uploaded on: </span>
            <span>September 18, 2020</span>
          </div>

          <div class="meta-data-item">
            <span class="meta-data-title">Uploaded by: </span>
            <span>User</span>
          </div>

          <div class="meta-data-item">
            <span class="meta-data-title">Uploaded to: </span>
            <span>Page Name</span>
          </div>

          <div class="meta-data-item">
            <span class="meta-data-title">Filename: </span>
            <span>WinterComeFeatured.jpg</span>
          </div>

          <div class="meta-data-item">
            <span class="meta-data-title">File type: </span>
            <span>image/jpeg</span>
          </div>

          <div class="meta-data-item">
            <span class="meta-data-title">File size: </span>
            <span>38 KB</span>
          </div>

          <div class="meta-data-item">
            <span class="meta-data-title">Dimensions: </span>
            <span>960 by 540 pixels</span>
          </div>
        </div>
        <form>
          <div class="form-group">
            <label for="exampleFormControlInput1">Email address</label>
            <input type="email" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
          </div>
          <div class="form-group">
            <label for="exampleFormControlSelect1">Example select</label>
            <select class="form-control" id="exampleFormControlSelect1">
              <option>1</option>
              <option>2</option>
              <option>3</option>
              <option>4</option>
              <option>5</option>
            </select>
          </div>
          <div class="form-group">
            <label for="exampleFormControlSelect2">Example multiple select</label>
            <select multiple class="form-control" id="exampleFormControlSelect2">
              <option>1</option>
              <option>2</option>
              <option>3</option>
              <option>4</option>
              <option>5</option>
            </select>
          </div>
          <div class="form-group">
            <label for="exampleFormControlTextarea1">Example textarea</label>
            <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
          </div>
        </form>
      </div>
    </div>
</div>
