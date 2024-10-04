<script lang="ts">

  import CloseIcon from "$lib/Utilities/Icons/CloseIcon.svelte";
  import { onMount, type Snippet } from 'svelte';
  import {beforeNavigate} from "$app/navigation";
  import {ModalType} from "$lib/Utilities/Modal/ModalType";

  type Props = {
    onModalConfirm: () => void,
    children: Snippet,
    modalType: ModalType,
  }

  let showPopup = $state(false);

  let {
    onModalConfirm,
    children,
    modalType
  }: Props = $props();


  let body: Element;

  function onModalClose() {
    showPopup = false;
  }

  function onModalConfirmClicked() {
    onModalConfirm();
    showPopup = false;
  }

  export function openModal() {
    showPopup = true;
  }


  onMount(async () => {
    body.classList.add("overflow-hidden");
  });

  beforeNavigate(() => {
    body.classList.remove("overflow-hidden");
  });

</script>
<svelte:body bind:this={body}/>

{#if showPopup}
  <div class="overlay"></div>
  <div class="reusable-modal-container">
    <div class="modal-header-reusable">
      <span class="text-white modal-header-title">Confirm</span>
      <button class="dashboard-icon" onclick={onModalClose}>
        <CloseIcon/>
      </button>
    </div>
    <div class="modal-content-reusable">
      <div class="content">
        {@render children()}
      </div>
      {#if modalType === ModalType.Action}
        <div class="modal-actions">
          <button class="btn btn-success" onclick={onModalClose}>Cancel</button>
          <button class="btn btn-danger" onclick={onModalConfirmClicked}>Confirm</button>
        </div>
      {/if}
    </div>
  </div>
{/if}
<style>
    .modal-content-reusable {
        display: flex;
        flex-direction: column;
        padding: 20px;
        background: var(--bs-body-bg);
        justify-content: center;
        align-items: center;
    }
    .modal-header-reusable {
        display: flex;
        flex-shrink: 0;
        align-items: center;
        justify-content: space-between;
        padding: 0.5rem 1rem;
        border-top-left-radius: calc(.3rem - 1px);
        border-top-right-radius: calc(.3rem - 1px);
        background: var(--bs-tertiary-bg);
        border: none !important;
    }

    .reusable-modal-container {
        position: absolute;
        /* bottom: 0; */
        /* height: 200px; */
        background: var(--bs-body-bg);
        margin: auto;
        padding-right: -30px;
        border-top-right-radius: 10px;
        border-top-left-radius: 10px;
        z-index: 1;
        left: 50%;
        top: 50%;
        transform: translate(-50%, -50%);
    }
</style>