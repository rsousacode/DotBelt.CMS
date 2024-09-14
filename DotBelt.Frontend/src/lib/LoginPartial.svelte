<script>
    import SignUpIcon from "$lib/admin/Icons/SignUpIcon.svelte";
    import LoginIcon from "$lib/admin/Icons/LoginIcon.svelte";
    import DashboardIcon from "$lib/admin/Icons/DashboardIcon.svelte";
</script>

<ul class="navbar-nav">
    <li class="nav-item">
        <a class="nav-link header-icon" href="/account/register"><SignUpIcon/></a>
    </li>
    <li class="nav-item">
        <a class="nav-link header-icon" href="/account/login"><LoginIcon/></a>
    </li>
    <li class="nav-item">
        <a class="nav-link header-icon" href="/my-admin"><DashboardIcon/></a>
    </li>

</ul>