<script>
    import LoginPartial from "$lib/LoginPartial.svelte";
</script>

<header>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-primary-header border-bottom box-shadow mb-3">
        <div class="container-fluid nav-container">
            <a class="navbar-brand brand-color"  href="/static">dotBelt CMS</a>
            <button class="navbar-toggler" type="button" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                <ul class="navbar-nav flex-grow-1">
                    <li class="nav-item">
                        <a class="nav-link text-white"  href="/static">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-white" href="/privacy">Privacy</a>
                    </li>
                </ul>
                <LoginPartial/>
            </div>
        </div>
    </nav>
</header>
<div class="container-fluid">
    <main class="pb-3">
        <slot />
    </main>
</div>

<footer class="border-top footer text-muted">
    <div class="container-fluid">
        &copy; 2024 - dotBelt CMS - <a href="/privacy">Privacy</a>
    </div>
</footer>

<style>
    .brand-color {
        background: #542fa9;
        color: white;
    }

    .navbar,
    .nav-container {
        padding-top: 0 !important;
        padding-bottom: 0 !important;
        padding-left: 0;
    }

    .navbar-brand {
        height: 100%;
        padding: 12px 24px;
    }

</style>
