<script lang="ts">
import {SITE_NAME} from "$lib/constants";

let model = $state({});

</script>

<svelte:head>
    <title>Register - {SITE_NAME} </title>
</svelte:head>

<h1>Register</h1>

<div class="row">
    <div class="col-md-4">
        <form id="registerForm" asp-route-returnUrl="{model.returnUrl}" method="post">
            <h2>Create a new account.</h2>
            <hr />
            <div asp-validation-summary="ModelOnly" class="text-danger" role="alert"></div>
            <div class="form-floating mb-3">
                <input for="Input.Email" class="form-control" autocomplete="username" aria-required="true" placeholder="name@example.com" />
                <label for="Input.Email">Email</label>
                <span asp-validation-for="Input.Email" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input for="Input.Password" class="form-control" autocomplete="new-password" aria-required="true" placeholder="password" />
                <label for="Input.Password">Password</label>
                <span asp-validation-for="Input.Password" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input for="Input.ConfirmPassword" class="form-control" autocomplete="new-password" aria-required="true" placeholder="password" />
                <label for="Input.ConfirmPassword">Confirm Password</label>
                <span asp-validation-for="Input.ConfirmPassword" class="text-danger"></span>
            </div>
            <button id="registerSubmit" type="submit" class="w-100 btn btn-lg btn-primary">Register</button>
        </form>
    </div>
    <div class="col-md-6 col-md-offset-2">
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
    </div>
</div>