<script lang="ts">
  import DashboardContainer from '$lib/Dashboard/DashboardContainer.svelte';
  import { onMount } from 'svelte';
  import { setDashboardData } from '$lib/Dashboard/DashboardStore.svelte';

  onMount(() => {
    setDashboardData({title: "Menus", subtitle: "Menu management"})
  })
</script>

<DashboardContainer>
  <div class="menu-items-container">
    <div class="menu-item-dropdown">
      <div class="menu-dropdown-title">
        Privacy Policy
      </div>
      <div class="menu-dropdown-form">
        <div class="mb-3">
          <label for="exampleFormControlInput1" class="form-label">Email address</label>
          <input type="email" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
        </div>
    <div class="mb-3">
          <label for="exampleFormControlInput1" class="form-label">Email address</label>
          <input type="email" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
        </div>
    <div class="mb-3">
          <label for="exampleFormControlInput1" class="form-label">Email address</label>
          <input type="email" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
        </div>

      </div>
    </div>
  </div>
</DashboardContainer>

<style>
  .menu-dropdown-title {
      background: var(--bs-secondary-bg);
      padding: 15px;
      border-radius: 8px;
      margin-bottom: 32px;
  }
</style>