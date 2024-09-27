<script lang="ts">
  import {AuthorizationState} from "$lib/Users/AuthorizationState";
  import type {SessionData} from "$lib/API/GraphQL/generated";
  import {page} from "$app/stores";
  import type { Snippet } from 'svelte';

  type Props = {state: AuthorizationState, children: Snippet}

  let {state, children} : Props = $props();

  let session = $derived<SessionData>($page.data.session);

</script>

{#if state === AuthorizationState.LoggedIn && session.isAuthenticated}
  {@render children()}
{/if}

{#if state === AuthorizationState.LoggedOut && !session.isAuthenticated}
  {@render children()}
{/if}

