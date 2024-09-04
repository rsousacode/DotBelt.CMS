<script lang="ts">
    import {onMount} from "svelte";
    import {beforeNavigate} from "$app/navigation";
    import CloseIcon from "$lib/CMS/Icons/CloseIcon.svelte";
    import ChevronLeft from "$lib/CMS/Icons/ChevronLeft.svelte";
    import ChevronRight from "$lib/CMS/Icons/ChevronRight.svelte";

    let body: Element;

    let {onClose, image} : {onClose : () => void, image: any} = $props();

    onMount(() => {
        body.classList.add("overflow-hidden");
    })

    beforeNavigate(() => {
        body.classList.remove("overflow-hidden");

    })

</script>

<svelte:body bind:this={body}/>
<div class="overlay"></div>
<div class="modal-container">
  <div class="modal-header">
    <div class="chevron-buttons">
      <button style="font-size: 24px;" class="dashboard-icon" onclick={onClose}><ChevronLeft/></button>
      <button style="font-size: 24px;" class="dashboard-icon" onclick={onClose}><ChevronRight/></button>
    </div>
    <span class="text-white modal-header-title"></span>

    <button class="dashboard-icon" onclick={onClose}><CloseIcon/></button>

  </div>
  <div class="modal-content">
    <div class="media-image-container">
      <img src={image.url} alt="">
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

<style>
    .meta-data-title {
        font-weight: bolder;
    }
    .overlay {
        background-color: rgb(0 0 0 / 57%);
        position: absolute;
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
    }
    .modal-header .modal-header-title {
        font-size: 18px;
    }
    .modal-header {
        display: flex;
        flex-shrink: 0;
        align-items: center;
        justify-content: space-between;
        padding: 1rem 1rem;
        border-top-left-radius: calc(.3rem - 1px);
        border-top-right-radius: calc(.3rem - 1px);
        background: #5d36b1;
        border: none !important;
    }
    .modal-container {
        position: absolute;
        top: 0;
        left: 0;
        bottom: 0;
        right: 0;
        background: white;
        margin: 30px;
        padding-right: -30px;
        border-top-right-radius: 10px;
        border-top-left-radius: 10px;
    }

    .modal-content {
        display: flex;
        flex-direction: row;
        padding: 20px;
        width: 100%;
        justify-content: space-evenly;
    }

    .media-image-container img,
    .media-image-container {
        max-width: 1000px;
    }

    .media-form-container {
        padding-left: 20px;
        width: 100%;
    }

    .image-meta-data {
        padding-bottom: 40px;
    }
</style>