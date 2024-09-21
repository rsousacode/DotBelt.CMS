<script lang="ts">

  import ChevronRight from "$lib/Utilities/Icons/ChevronRight.svelte";
  import CloseIcon from "$lib/Utilities/Icons/CloseIcon.svelte";
  import ChevronLeft from "$lib/Utilities/Icons/ChevronLeft.svelte";
  import {onMount} from "svelte";
  import {beforeNavigate} from "$app/navigation";
  import type {MouseEventHandler} from "svelte/elements";

  type Props = {
    showPopup: boolean,
    onClose: () => MouseEventHandler<HTMLButtonElement> | null | undefined,
    children: any
  }
  let {onClose = () => null, children} : Props = $props();
  let body: Element;


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
    {@render children()}
  </div>
</div>

