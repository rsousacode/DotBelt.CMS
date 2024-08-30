<script>
    import LoginPartial from "$lib/LoginPartial.svelte";
</script>

<header>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div class="container">
            <a class="navbar-brand"  href="/">WebAuth</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                <ul class="navbar-nav flex-grow-1">
                    <li class="nav-item">
                        <a class="nav-link text-dark"  href="/">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-dark" href="/privacy">Privacy</a>
                    </li>
                </ul>
                <LoginPartial/>
            </div>
        </div>
    </nav>
</header>
<div class="container">
    <main class="pb-3">
        <slot />
    </main>
</div>

<footer class="border-top footer text-muted">
    <div class="container">
        &copy; 2024 - WebAuth - <a href="/Privacy">Privacy</a>
    </div>
</footer>


