<script lang="ts">

  import {goto} from "$app/navigation";
  import {SITE_NAME} from "$lib/constants";

  async function onLoginClicked() {
      await goto("/account/login");
  }
</script>

<svelte:head>
  <title>Welcome - {SITE_NAME} </title>
</svelte:head>
<div class="container">
  <main class="mt-4 align-items-center justify-content-center flex-column d-flex success-register">
    <h5>Thank you for registering! </h5>
    <p>Please check your email and click on the confirmation link to activate your account.</p>
    <button type="button" class="btn btn-primary" onclick={onLoginClicked}>Login</button>

  </main>
</div>

