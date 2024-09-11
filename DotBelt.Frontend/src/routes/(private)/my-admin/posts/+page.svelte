<script lang="ts">
    import DashboardContainer from "$lib/admin/Dashboard/DashboardContainer.svelte";
    import {SITE_NAME} from "$lib/constants";
    import ApolloClientProvider from "$lib/GraphQL/ApolloClientProvider.svelte";
    import {getPosts} from "$lib/Queries/GetPosts";
    import {type PostsConnection, PostTypeEnum} from "$lib/GraphQL/generated";
    import type {Maybe} from "yup";
    import EditIcon from "$lib/admin/Icons/EditIcon.svelte";
    import ViewPublishedIcon from "$lib/admin/Icons/ViewPublishedIcon.svelte";
    import {page} from "$app/stores";
    import {afterNavigate} from "$app/navigation";
    import type {PaginationQuery} from "$lib/GraphQL/PaginationQuery";
    import {onMount} from "svelte";
    import {setDashboardData, updateDashboardData} from "$lib/admin/Dashboard/DashboardStore.svelte";

    let apolloClientProvider: ApolloClientProvider;
    let postsResult: Maybe<PostsConnection> | undefined = $state(undefined);
    let postsPerPage = $state(5);

    onMount(() => {
        setDashboardData({title: "Posts", subtitle: ""})
    })

    async function fetchPosts(postType: string, variables: PaginationQuery) {
        const apolloClient = apolloClientProvider.GetApolloSvelteClient();
        if (!apolloClient) {
            console.error("API Client not available");
            return;
        }
        postsResult = await getPosts(apolloClient, postType.toUpperCase() as PostTypeEnum, variables);
        console.log(postsResult);
    }

    afterNavigate(async () => {
        const postType = $page.url.searchParams.get("type");
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
        const postType = $page.url.searchParams.get("type");
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
        const postType = $page.url.searchParams.get("type");
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

<ApolloClientProvider bind:this={apolloClientProvider}>
  <DashboardContainer>
    {#if postsResult}
      <div class="table-responsive">
        <table class="table">
          <thead>
          <tr>
            <th scope="col">Title</th>
            <th scope="col">Published Date</th>
            <th scope="col"></th>
          </tr>
          </thead>
          <tbody>

          {#if postsResult.nodes.length > 0}
            {#each postsResult.nodes as post}
              <tr>
                <td>{post.title}</td>
                <td>{post.publishDate}</td>
                <td class="table-actions">
                  <a href={`/my-admin/posts/${post.id}`} target="_blank" class="cms-action-icon">
                    <EditIcon/>
                  </a>
                  <a class="cms-action-icon" target="_blank" href={`/${post.urlName}`}>
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
</ApolloClientProvider>

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