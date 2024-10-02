<script lang="ts">
  import type {Maybe, PostResponse} from "$lib/API/GraphQL/generated";

  let {hasImage = false, posts = $bindable()}: Props = $props();

  type Props = { hasImage: boolean, posts: Maybe<PostResponse[]> };

</script>

{#if posts}
  <div class="posts-container">

    {#each posts as post}
      <div class="post-item">
        {#if hasImage}
          <div class="post-image">
            <img alt={post.featuredImage?.altText} src={post.featuredImage ? `/${post.featuredImage.relativeUrl}` : "/images/placeholder-image.png" }/>
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