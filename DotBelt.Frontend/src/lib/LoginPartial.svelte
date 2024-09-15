<script lang="ts">
    import DashboardIcon from "$lib/Utilities/Icons/DashboardIcon.svelte";
    import ColorSwitcher from "$lib/Utilities/ColorSwitcher.svelte";
    import {page} from "$app/stores";
    import type { SessionData } from "$lib/API/GraphQL/generated";
    import AccountDropdown from "$lib/Users/AccountDropdown.svelte";
    let session = $derived<SessionData>($page.data.session);

</script>


<ul class="navbar-nav">
    {#if session.isAuthenticated}
        <li class="nav-item">
            <a class="nav-link header-icon" href="/my-admin"><DashboardIcon/></a>
        </li>
    {/if}
    <AccountDropdown/>
    <ColorSwitcher/>
</ul>
