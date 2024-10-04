<script lang="ts">
  import { onMount } from 'svelte';
  import { apolloClientStore } from '$lib/API/GraphQL/apolloClientStore';
  import type { RepeaterBlockData } from '$lib/Content/EditorJS/Blocks/repeater/RepeaterBlockData';
  import type { DotBeltQuery, Maybe, PostsConnection } from '$lib/API/GraphQL/generated';
  import { gql } from '@apollo/client/core';

  type Props = {block: RepeaterBlockData};

  let {block} : Props = $props();

  let postsData: Maybe<PostsConnection> | undefined = $state();

  onMount(async () => {
    const client = apolloClientStore.getClient();

    const {data: {posts}} = await client.query<DotBeltQuery>({
      query: gql(block.query),
      variables: block.variables,
    })
    postsData = posts;

  });


</script>

{#if postsData}

  <div class="posts-container">

    {#each postsData.nodes as post}
      <div class="card" style="width: 18rem;">
        <img alt={post.featuredImage?.altText} src={post.featuredImage ? `/${post.featuredImage.relativeUrl}` : "/images/placeholder-image.png" } class="card-img-top">
        <div class="card-body">
          <h5 class="card-title">{post.title}</h5>
          <p class="card-text">{post.description}</p>
        </div>
      </div>
    {/each}


  </div>

{/if}