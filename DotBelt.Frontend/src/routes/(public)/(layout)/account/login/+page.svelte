<script lang="ts" context="module">
  import * as yup from "yup";

  const loginSchema = yup.object({
    email: yup.string()
      .email("the email address must be valid")
      .required(" "),
    password: yup
      .string()
      .min(8, "the password has to be minimum 8 characters")
      .required(" "),
    rememberMe: yup
      .string()
      .nullable()

  });


</script>

<script lang="ts">
  import {SITE_NAME} from "$lib/constants";

  import {onMount} from "svelte";
  import {page} from "$app/stores";
  import {Api} from "$lib/API/Swagger/generated/Api";
  import {goto, invalidateAll} from "$app/navigation";
  import YupForm from "$lib/Utilities/YupForm.svelte";

  let model = $state({});
  let yupForm: YupForm;

  async function handleSubmit(form, data: { email: string, password: string, rememberMe: null | string }) {
    const beltCms = new Api();

    beltCms.api.authLoginCreate({
      password: data.password,
      email: data.email
    }, {useCookies: true})
      .then(async () => {
        await goto("/")
        await invalidateAll();
      }).catch(async err => {
      const errors = await err.json();
      yupForm.onAspNetErrors(errors);
    })


  }

</script>

<svelte:head>
  <title>Login - {SITE_NAME} </title>
</svelte:head>


<h1>Log in</h1>
<div class="row">
  <div class="col-md-4">
    <section>
      <YupForm bind:this={yupForm} schema={loginSchema} onValidSubmit={handleSubmit}>
        <h2>Use a local account to log in.</h2>
        <hr/>
        <div class="form-floating mb-3">
          <input name="email" class="form-control" autocomplete="username" aria-required="true"
                 placeholder="name@example.com"/>
          <label for="email" class="form-label">Email</label>
          <div data-error-for="email" class="text-danger"></div>
          <div data-server-error-for="email" class="text-danger"></div>
        </div>
        <div class="form-floating mb-3">
          <input type="password" id="password" name="password" class="form-control" autocomplete="current-password"
                 aria-required="true"
                 placeholder="password"/>
          <label for="password" class="form-label">Password</label>
          <div data-error-for="password" class="text-danger"></div>
          <div data-server-error-for="password" class="text-danger"></div>
        </div>
        <div class="checkbox mb-3">
          <label class="form-label">
            <input name="rememberMe" class="form-check-input" type="checkbox" value="" id="flexCheckDefault">
            Remember Me
          </label>
          <div data-error-for="rememberMe" class="text-danger"></div>
          <div data-server-error-for="rememberMe" class="text-danger"></div>
        </div>
        <div>
          <button id="login-submit" type="submit" class="w-100 btn btn-lg btn-primary">Log in</button>
        </div>
        <div>
          <p><a id="forgot-password" href="./forgotPassword">Forgot your password?</a></p>
          <p><a href="./register">Register as a new user</a></p>
          <p><a id="resend-confirmation" href="./ResendEmailConfirmation">Resend email confirmation</a></p>
        </div>
      </YupForm>
    </section>
  </div>
  <div class="col-md-6 col-md-offset-2">
    <section>
      <h3>Use another service to log in.</h3>
      <hr/>
      {#if (model?.externalLogins?.count ?? 0) == 0}
        <div>
          <p>
            There are no external authentication services configured. See this <a
            href="https://go.microsoft.com/fwlink/?LinkID=532715">article
            about setting up this ASP.NET application to support logging in via external services</a>.
          </p>
        </div>
      {:else}
        <form id="external-account" href="./ExternalLogin" asp-route-returnUrl="{model.returnUrl}" method="post"
              class="form-horizontal">
          <div>
            <p>
              {#each model?.externalLogins as provider }
                <button type="submit" class="btn btn-primary" name="provider" value="@provider.Name"
                        title="Log in using your @provider.DisplayName account">@provider.DisplayName
                </button>

              {/each}
            </p>
          </div>
        </form>
      {/if}
    </section>
  </div>
</div>
