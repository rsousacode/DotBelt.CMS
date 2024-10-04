<script lang="ts">
  import DashboardContainer from '$lib/Dashboard/DashboardContainer.svelte';
  import { SITE_NAME } from '$lib/constants';
  import { getPosts } from '$lib/Content/Posts/GetPosts';
  import { type PostsConnection, PostTypeEnum } from '$lib/API/GraphQL/generated';
  import type { Maybe } from 'yup';
  import EditIcon from '$lib/Utilities/Icons/EditIcon.svelte';
  import ViewPublishedIcon from '$lib/Utilities/Icons/ViewPublishedIcon.svelte';
  import { page } from '$app/stores';
  import { afterNavigate } from '$app/navigation';
  import type { PaginationQuery } from '$lib/API/GraphQL/PaginationQuery';
  import { onMount } from 'svelte';
  import { setDashboardData, updateDashboardFragment } from '$lib/Dashboard/DashboardStore.svelte';
  import { apolloClientStore } from '$lib/API/GraphQL/apolloClientStore';
  import dayjs from 'dayjs';

  let postsResult: Maybe<PostsConnection> | undefined = $state(undefined);
    let postsPerPage = $state(5);

    let postType : string = $state("post");

    function initialization() {
      const type = $page.url.searchParams.get("type");

      if(type) {
        postType = type;
      } else {
        console.error("Unable to retrieve post type")
      }

      setDashboardData({title: postType === "post" ? "Posts" : "Pages", subtitle: ""})
      updateDashboardFragment(newPostButton);
    }

    onMount(() => {
      initialization();
    })

    async function fetchPosts(postType: string, variables: PaginationQuery) {
        const apolloClient = apolloClientStore.getClient();
        if (!apolloClient) {
            console.error("API Client not available");
            return;
        }
        postsResult = await getPosts(apolloClient, postType.toUpperCase() as PostTypeEnum, variables);
    }

    afterNavigate(async () => {
      initialization();
        if (postType) {
            await fetchPosts(postType, {
                first: postsPerPage,
                last: undefined,
                before: undefined,
                after: undefined,
            });
        }
    });

    async function onNextPageClicked() {
        if (!postsResult) return;
        if (postType) {
            await fetchPosts(postType, {
                first: postsPerPage,
                last: undefined,
                before: undefined,
                after: postsResult.pageInfo.endCursor,
            });
        }
    }


    async function onPreviousPageClicked() {
        if (!postsResult) return;
        if (postType) {
            await fetchPosts(postType, {
                first: undefined,
                last: postsPerPage,
                before: postsResult.pageInfo.startCursor,
                after: undefined,
            });
        }
    }
</script>

<svelte:head>
  <title>Posts - {SITE_NAME} </title>
</svelte:head>

{#snippet newPostButton()}
  <a href={`/my-admin/posts/new?type=${$page.url.searchParams.get("type")}`} class="btn btn-primary" type="button" >
    Add new
  </a>

{/snippet}

<DashboardContainer>
  {#if postsResult}
    <div class="table-responsive mt-4">
      <table class="table">
        <thead>
        <tr>
          <th scope="col">Title</th>
          <th scope="col">Published Date</th>
          <th scope="col">Status</th>
          <th scope="col">Author</th>
          <th scope="col"></th>
        </tr>
        </thead>
        <tbody>

        {#if postsResult?.nodes?.length &&  postsResult.nodes.length > 0}
          {#each postsResult.nodes as post}
            <tr>
              <td>{post.title === "" ? "No title" : post.title}</td>
              <td>{dayjs(post.publishDate).toString()}</td>
              <td>{post.status}</td>
              <td>{post.author?.userName}</td>
              <td class="table-actions">
                <a href={`/my-admin/posts/${post.id}`} target="_blank" class="cms-action-icon">
                  <EditIcon/>
                </a>
                <a class="cms-action-icon" target="_blank" href={`/${post.relativeUrl}`}>
                  <ViewPublishedIcon/>
                </a>
              </td>
            </tr>
          {/each}
        {:else}
          <tr>
            <td colspan="6" style="height: 250px;">
              No data found
            </td>
          </tr>

        {/if}

        </tbody>
      </table>
    </div>
    <button onclick={onPreviousPageClicked} disabled="{!postsResult.pageInfo.hasPreviousPage}">Previous page</button>
    <button onclick={onNextPageClicked} disabled="{!postsResult.pageInfo.hasNextPage}">Next Page</button>
  {/if}
</DashboardContainer>

<style>
    .table-actions {
        display: flex;
    }
    .cms-action-icon {
        font-size: 20px;
        padding: 3px;
        color: inherit;
    }
</style>