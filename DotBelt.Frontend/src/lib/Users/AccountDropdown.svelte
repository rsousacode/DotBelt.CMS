<script lang="ts">
  import AccountIcon from "$lib/Utilities/Icons/AccountIcon.svelte";
  import {Api} from "$lib/API/Swagger/generated/Api";
  import {goto, invalidateAll} from "$app/navigation";
  import Authorize from "$lib/Users/Authorize.svelte";
  import {AuthorizationState} from "$lib/Users/AuthorizationState";


  async function onLogoutClicked() {
    const beltCms = new Api();
    await beltCms.api
      .authLogoutCreate()
      .then(async () => {
        await invalidateAll();
        await goto("/")

      })
  }
</script>

<svelte:head>
  <link rel="stylesheet" href="/site.css"/>
</svelte:head>

<li class="nav-item dropdown d-flex">
  <button class="btn btn-link nav-link py-2 px-0 px-lg-2 dropdown-toggle d-flex align-items-center"
          id="bd-theme"
          type="button"
          aria-expanded="false"
          data-bs-toggle="dropdown"
          data-bs-display="static"
          aria-label="Toggle theme (auto)">
    <span class="account-icon">
          <AccountIcon/>
    </span>

    <span class="d-lg-none ms-2" id="bd-theme-text">Account</span>
  </button>
  <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="bd-theme-text">
    <Authorize state={AuthorizationState.LoggedIn}>
      <li>
        <a href="/account/manage" type="button" class="btn dropdown-item d-flex align-items-center">
          Account
        </a>
      </li>
      <li>
        <button onclick={onLogoutClicked} type="button" class="dropdown-item d-flex align-items-center"
                aria-pressed="false">

          Logout

        </button>
      </li>


    </Authorize>
    <Authorize state={AuthorizationState.LoggedOut}>
      <li>
        <a href="/account/login" type="button" class="btn dropdown-item d-flex align-items-center">
          Login
        </a>
      </li>
      <li>
        <a href="/account/register" class="btn dropdown-item d-flex align-items-center">
          Register
        </a>
      </li>
    </Authorize>


  </ul>
</li>
