<script>
    import SignUpIcon from "$lib/Utilities/Icons/SignUpIcon.svelte";
    import LoginIcon from "$lib/Utilities/Icons/LoginIcon.svelte";
    import DashboardIcon from "$lib/Utilities/Icons/DashboardIcon.svelte";
    import ColorSwitcher from "$lib/Utilities/ColorSwitcher.svelte";
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

    <ColorSwitcher/>
</ul>
