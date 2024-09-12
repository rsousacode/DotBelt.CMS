<script lang="ts">
  import {onMount} from "svelte";
  import {getApolloClient} from "$lib/GraphQL/apolloClient";
  import type {ApolloClient, NormalizedCacheObject} from "@apollo/client/core/index.js";

  let client: ApolloClient<NormalizedCacheObject> | null = null;

  onMount(() => {
    if (client === null) {
      initializeClient();
    }
    globalThis.GetApolloSvelteClient = GetApolloSvelteClient;
  });

  function initializeClient() {
    client = getApolloClient();
  }

  export function GetApolloSvelteClient() {
    if (client === null) {
      initializeClient();
    }
    return client;
  }

</script>

<slot/>