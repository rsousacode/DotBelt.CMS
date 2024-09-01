<script lang="ts">
    import {gql} from "@apollo/client/core";
    import {type GraphQlQuery, type Maybe, type PostsConnection, PostTypeEnum} from "$lib/GraphQL/generated";
    import {onMount} from "svelte";

    let { hasImage = false, postCount = 10, posts } = $props();

    let postsResult : Maybe<PostsConnection> | undefined = $state({});

    let nodes = $derived(postsResult?.nodes);

    onMount(async () => {
        console.log('im mounted')
        console.log(posts)

    })



</script>

{#if posts}
  <div class="posts-container">

    {#each posts as post}
      <div class="post-item">
        {#if hasImage}
          <div class="post-image">
            <img alt="place-holder image" src="/images/placeholder-image.png" />
          </div>
        {/if}

        <div class="post-title">
          <h3>{post.title}</h3>
        </div>
        <div class="post-description">
          <p>
            {post.description}
          </p>
        </div>
      </div>
    {/each}


  </div>

{/if}

<style>
  .posts-container {
      display: flex;
      flex-wrap: wrap;
      margin-top: 1rem;
      margin-bottom: 1rem;
  }
  .post-item {
      display: flex;
      flex-direction: column;
      max-width: 320px;
      padding: 15px;
  }

  .post-image {
      margin-bottom: 20px;
  }
  .post-image > img {
      max-width: 100%;
  }
</style>