<script lang="ts">
    import {SITE_NAME} from "$lib/constants";

    import * as yup from 'yup';
    import {onMount} from "svelte";
    import YupForm from "$lib/YupForm.svelte";
    import {Api} from "$lib/Swagger/generated/Api";

    const schema = yup.object().shape({
        email: yup.string().email('Invalid email address').required('Email is required'),
        password: yup.string().required('Password is required'),
        confirmPassword: yup.string().oneOf([yup.ref('password'), null], 'Passwords must match').required('Confirm password is required')
    });

    async function handleSubmit(form, data) {
        const beltCms = new Api();

        beltCms.api.authRegisterCreate({
            email: data.email,
            password: data.password,
        }).then(async successData => {
            console.log('successData', successData)

        }).catch(async err => {
            const error = await err.json();
            console.log('error', error)
        })


    }

    onMount(() => {
        console.log(schema);
    })
</script>

<svelte:head>
  <title>Register - {SITE_NAME} </title>
</svelte:head>

<h1>Register</h1>

<div class="row">
  <div class="col-md-4">
    <YupForm schema={schema} onValidSubmit={handleSubmit}>
      <h2>Create a new account.</h2>
      <hr/>
      <div class="text-danger" role="alert"></div>
      <div class="form-floating mb-3">
        <input name="email" class="form-control" autocomplete="username" aria-required="true"
               placeholder="name@example.com"/>
        <label for="email">Email</label>
        <span data-error-for="email" class="text-danger"></span>
      </div>
      <div class="form-floating mb-3">
        <input type="password" name="password" class="form-control" autocomplete="new-password" aria-required="true"
               placeholder="password"/>
        <label for="password">Password</label>
        <span data-error-for="password" class="text-danger"></span>
      </div>
      <div class="form-floating mb-3">
        <input type="password" name="confirmPassword" class="form-control" autocomplete="new-password" aria-required="true"
               placeholder="password"/>
        <label for="confirmPassword">Confirm Password</label>
        <span data-error-for="confirmPassword" class="text-danger"></span>
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