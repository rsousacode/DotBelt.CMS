<script>
    import SignUpIcon from "$lib/CMS/Icons/SignUpIcon.svelte";
    import LoginIcon from "$lib/CMS/Icons/LoginIcon.svelte";
    import DashboardIcon from "$lib/CMS/Icons/DashboardIcon.svelte";
</script>

<ul class="navbar-nav">
    <li class="nav-item">
        <a class="nav-link text-white header-icon" href="/account/register"><SignUpIcon/></a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-white header-icon" href="/account/login"><LoginIcon/></a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-white header-icon" href="/my-admin"><DashboardIcon/></a>
    </li>

</ul>