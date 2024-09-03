<script>
    import LoginPartial from "$lib/LoginPartial.svelte";
</script>

<header>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-primary-header box-shadow">
        <div class="container-fluid nav-container">
            <a class="navbar-brand brand-color"  href="/">.Belt CMS</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                <ul class="navbar-nav flex-grow-1">

                </ul>
                <LoginPartial/>
            </div>
        </div>
    </nav>
</header>
    <main>
        <slot />
    </main>


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
        width: 225px;
        justify-content: center;
        align-items: center;
        display: flex;
    }

</style>
