<script lang="ts">
  import DashboardContainer from '$lib/Dashboard/DashboardContainer.svelte';
  import { onMount } from 'svelte';
  import { setDashboardData, updateDashboardFragment } from '$lib/Dashboard/DashboardStore.svelte';
  import { page } from '$app/stores';
  import type { PostResponse, TenantResponse } from '$lib/API/GraphQL/generated';
  import SaveIcon from '$lib/Utilities/Icons/SaveIcon.svelte';
  import SuccessIcon from '$lib/Utilities/Icons/SuccessIcon.svelte';
  import TransitionalIcon from '$lib/Utilities/Icons/TransitionalIcon.svelte';
  import { gql } from '@apollo/client/core';
  import { apolloClientStore } from '$lib/API/GraphQL/apolloClientStore';

  let { data: { tenant, posts } } = $page;

  let tenantData : TenantResponse = $state( tenant as TenantResponse);

  let postsData : PostResponse[] = posts as PostResponse[];

  let allowedFileTypes : string = $state("");

  type OnChangeEvent = Event & { currentTarget: HTMLInputElement };

  let successFeedbackIcon: TransitionalIcon;

  onMount(() => {
    tenantData = tenant as TenantResponse;
    setDashboardData({title: "Settings - Reading", subtitle: "Tenant Settings"})
    updateDashboardFragment(saveButton);

    allowedFileTypes = tenantData.allowedFileTypes.join(",");
  })

  function onAllowedFileTypesChanged(e: OnChangeEvent) {

    allowedFileTypes = e.currentTarget.value;
    tenantData.allowedFileTypes = allowedFileTypes
      .split(",")
      .map(x => x.trim());

  }

  async function saveData() {

    const mutation = gql`
      mutation changeTenant($input: TenantResponseInput!) {
          editTenant(input: {tenant: $input}) {
            tenantResponse {
              id,
              homepageId,
              allowedFileTypes
            }
          }
      }
`;

    const client = apolloClientStore.getClient();

    await client.mutate({
      mutation: mutation,
      variables: {input: tenantData}
    })

    successFeedbackIcon.triggerComponent2();

  }
</script>

{#snippet saveButton()}
  <button class="dashboard-icon" type="button" onclick={saveData}>
    <TransitionalIcon
      bind:this={successFeedbackIcon}
      component1={SaveIcon}
      component2={SuccessIcon} />
  </button>
{/snippet}

<DashboardContainer>
  <div class="reading-settings-container mt-5 px-3">
    <div class="mb-3">
      <label for="tenant-name" class="form-label">Tenant Name</label>
      <input bind:value={tenantData.name} type="email" class="form-control" id="tenant-name">
    </div>
    <div class="mb-3">
      <label for="full-rul" class="form-label">Full Url</label>
      <input bind:value={tenantData.fullUrl} type="text" class="form-control" id="full-url" >
    </div>
    <div class="mb-3">
      <label for="full-rul" class="form-label">Allowed File Types</label>
      <input onchange={onAllowedFileTypesChanged} value={allowedFileTypes} type="text" class="form-control" id="full-url" >
    </div>
    <div class="mb-3">
      <label for="exampleFormControlInput1" class="form-label">Home page</label>
      <select bind:value={tenantData.homepageId} class="form-select" aria-label="Default select example">
        <option selected>Select an option</option>
        {#each postsData as post}
          <option value={post.id}>{post.title}</option>
        {/each}
      </select>
    </div>

  </div>
</DashboardContainer>

<style>
  .reading-settings-container {
    max-width: 600px;
  }
</style>