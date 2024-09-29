<script lang="ts">
  import DashboardContainer from "$lib/Dashboard/DashboardContainer.svelte";
  import {SITE_NAME} from "$lib/constants";
  import {type UsersConnection} from "$lib/API/GraphQL/generated";
  import type {Maybe} from "yup";
  import EditIcon from "$lib/Utilities/Icons/EditIcon.svelte";
  import type {PaginationQuery} from "$lib/API/GraphQL/PaginationQuery";
  import {onMount} from "svelte";
  import {setDashboardData} from "$lib/Dashboard/DashboardStore.svelte";
  import {getUsers} from "$lib/Users/GetUsers";
  import {apolloClientStore} from "$lib/API/GraphQL/apolloClientStore";

  let usersResult: Maybe<UsersConnection> | undefined = $state(undefined);
  let usersPerPage = $state(5);

  onMount(async () => {
    setDashboardData({title: "Users", subtitle: "Users Management"})
    await fetchUsers({
      first: usersPerPage,
      last: undefined,
      before: undefined,
      after: undefined,
    });
  })

  async function fetchUsers(variables: PaginationQuery) {
    const apolloClient = apolloClientStore.getClient();
    if (!apolloClient) {
      console.error("API Client not available");
      return;
    }
    usersResult = await getUsers(apolloClient, variables);
  }

  async function onNextPageClicked() {
    if (!usersResult) return;

    await fetchUsers({
      first: usersPerPage,
      last: undefined,
      before: undefined,
      after: usersResult.pageInfo.endCursor,
    });

  }

  async function onPreviousPageClicked() {
    if (!usersResult) return;
    await fetchUsers({
      first: undefined,
      last: usersPerPage,
      before: usersResult.pageInfo.startCursor,
      after: undefined,
    });
  }
</script>

<svelte:head>
  <title>Posts - {SITE_NAME} </title>
</svelte:head>

<DashboardContainer>
  {#if usersResult}
    <div class="table-responsive">
      <table class="table">
        <thead>
        <tr>
          <th scope="col">UserName</th>
          <th scope="col">Email</th>
          <th scope="col">Email Confirmed</th>
          <th scope="col"></th>
        </tr>
        </thead>
        <tbody>

        {#if usersResult.nodes?.length && usersResult.nodes?.length > 0}
          {#each usersResult.nodes as user}
            <tr>
              <td>{user.userName}</td>
              <td>{user.email}</td>
              <td>{user.emailConfirmed}</td>

              <td class="table-actions">
                <a href={`/my-admin/settings/users/${user.id}`} target="_blank" class="cms-action-icon">
                  <EditIcon/>
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
    <button onclick={onPreviousPageClicked} disabled="{!usersResult.pageInfo.hasPreviousPage}">Previous page</button>
    <button onclick={onNextPageClicked} disabled="{!usersResult.pageInfo.hasNextPage}">Next Page</button>
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