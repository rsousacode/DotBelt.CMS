// $lib/API/GraphQL/apolloClientStore.ts
import { writable, get } from 'svelte/store';
import { getApolloClient } from './apolloClient';
import type { ApolloClient, NormalizedCacheObject } from '@apollo/client/core';

const clientStore = writable<ApolloClient<NormalizedCacheObject> | null>(null);

function initializeClient() {
  const currentClient = get(clientStore);
  if (!currentClient) {
    const newClient = getApolloClient();
    clientStore.set(newClient);
    return newClient;
  }
  return currentClient;
}

export const apolloClientStore = {
  getClient: initializeClient,
};