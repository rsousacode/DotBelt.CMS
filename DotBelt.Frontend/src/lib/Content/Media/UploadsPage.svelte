<script lang="ts">
  import DashboardContainer from '$lib/Dashboard/DashboardContainer.svelte';
  import type { Snippet } from 'svelte';

  let {children}: {children: Snippet} = $props();
</script>

<DashboardContainer>
  {@render children()}
</DashboardContainer>
