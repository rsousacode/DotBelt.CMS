<script lang="ts" context="module">
  import * as yup from 'yup';
  import {ref} from "yup";

  const registerSchema = yup.object({
    email: yup.string().email('Invalid email address').required('Email is required'),
    password: yup.string().required('Password is required'),
    confirmPassword: yup
      .string()
      .oneOf([ref("password")], 'Passwords must match')
      .required(" "),
  });

</script>
<script lang="ts">
    import {SITE_NAME} from "$lib/constants";

    import {onMount} from "svelte";
    import YupForm from "$lib/Utilities/YupForm.svelte";
    import {Api} from "$lib/API/Swagger/generated/Api";
    import {goto} from "$app/navigation";

    let yupForm : YupForm;


    async function handleSubmit(form, data) {
        const beltCms = new Api();

        beltCms.api.authRegisterCreate({
            email: data.email,
            password: data.password,
        }).then(async successData => {
            await goto("/account/register/success")
        }).catch(async err => {
            const errors = await err.json();
            yupForm.onAspNetErrors(errors);
        })


    }

</script>

<svelte:head>
  <title>Thank you for registering - {SITE_NAME} </title>
</svelte:head>

<h1>Register</h1>

<div class="row">
  <div class="col-md-4">
    <YupForm bind:this={yupForm} schema={registerSchema} onValidSubmit={handleSubmit}>
      <h2>Create a new account.</h2>
      <hr/>
      <div data-server-errors class="text-danger" role="alert"></div>
      <div class="form-floating mb-3">
        <input name="email" class="form-control" autocomplete="username" aria-required="true"
               placeholder="name@example.com"/>
        <label for="email">Email</label>
        <div data-error-for="email" class="text-danger"></div>
        <div data-server-error-for="email" class="text-danger"></div>
      </div>
      <div class="form-floating mb-3">
        <input type="password" name="password" class="form-control" autocomplete="new-password" aria-required="true"
               placeholder="password"/>
        <label for="password">Password</label>
        <div data-error-for="password" class="text-danger"></div>
        <div data-server-error-for="password" class="text-danger"></div>
      </div>
      <div class="form-floating mb-3">
        <input type="password" name="confirmPassword" class="form-control" autocomplete="new-password" aria-required="true"
               placeholder="password"/>
        <label for="confirmPassword">Confirm Password</label>
        <div data-error-for="confirmPassword" class="text-danger"></div>
        <div data-server-error-for="confirmPassword" class="text-danger"></div>
      </div>
      <button id="registerSubmit" type="submit" class="w-100 btn btn-lg btn-primary">Register</button>

    </YupForm>
  </div>
  <!--    <div class="col-md-6 col-md-offset-2">
          <section>
              <h3>Use another service to register.</h3>
              <hr />

              {#if (model?.externalLogins?.count ?? 0) == 0}
                  <div>
                      <p>
                          There are no external authentication services configured. See this <a href="https://go.microsoft.com/fwlink/?LinkID=532715">article
                          about setting up this ASP.NET application to support logging in via external services</a>.
                      </p>
                  </div>
              {:else}
                  <form id="external-account" href="./ExternalLogin" asp-route-returnUrl="{model.returnUrl}" method="post" class="form-horizontal">
                      <div>
                          <p>
                              {#each model?.externalLogins as provider}
                                  <button type="submit" class="btn btn-primary" name="provider" value="@provider.Name" title="Log in using your {provider.displayName} account">{provider.displayName}</button>

                              {/each}
                          </p>
                      </div>
                  </form>
              {/if}
          </section>
      </div>-->
</div>