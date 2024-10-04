<script lang="ts">
  import DashboardContainer from '$lib/Dashboard/DashboardContainer.svelte';
  import { onMount } from 'svelte';
  import { setDashboardData } from '$lib/Dashboard/DashboardStore.svelte';

  onMount(() => {
    setDashboardData({title: "Settings - Reading", subtitle: "Tenant Settings"})
  })
</script>

<DashboardContainer>
  <div class="reading-settings-container mt-5 px-3">
    <div class="mb-3">
      <label for="exampleFormControlInput1" class="form-label">Tenant Name</label>
      <input type="email" class="form-control" id="exampleFormControlInput1">
    </div>
    <div class="mb-3">
      <label for="exampleFormControlInput1" class="form-label">Full Url</label>
      <input type="email" class="form-control" id="exampleFormControlInput1" >
    </div>
    <div class="mb-3">
      <label for="exampleFormControlInput1" class="form-label">Home page</label>
      <select class="form-select" aria-label="Default select example">
        <option selected>Select an option</option>
        <option value="1">One</option>
        <option value="2">Two</option>
        <option value="3">Three</option>
      </select>
    </div>

  </div>
</DashboardContainer>

<style>
  .reading-settings-container {
    max-width: 600px;
  }
</style>