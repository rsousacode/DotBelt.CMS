<script lang="ts">
import {SITE_NAME} from "$lib/constants";
import * as yup from "yup";
import {ref} from "yup";

let model = $state({});

const schema = yup.object({
    email: yup.string()
        .email("the email address must be valid")
        .required(" "),
    name: yup.string()
        .required("O nome é um campo obrigatório"),
    password: yup
        .string()
        .min(8, "the password has to be minimum 8 characters")
        .required(" "),
    passwordConfirmation: yup
        .string()
        .min(8, "the password has to be minimum 8 characters")
        .oneOf([ref("password")], 'Passwords must match')
        .required(" "),
    acceptTermsAndConditions: yup
        .bool()
        .required(" ")

});

</script>

<svelte:head>
    <title>Login - {SITE_NAME} </title>
</svelte:head>


<h1>Log in</h1>
<div class="row">
    <div class="col-md-4">
        <section>
            <form id="account" method="post">
                <h2>Use a local account to log in.</h2>
                <hr />
                <div  class="text-danger" role="alert"></div>
                <div class="form-floating mb-3">
                    <input for="Input.Email" class="form-control" autocomplete="username" aria-required="true" placeholder="name@example.com" />
                    <label for="Input.Email" class="form-label">Email</label>
                    <span asp-validation-for="Input.Email" class="text-danger"></span>
                </div>
                <div class="form-floating mb-3">
                    <input asp-for="Input.Password" class="form-control" autocomplete="current-password" aria-required="true" placeholder="password" />
                    <label asp-for="Input.Password" class="form-label">Password</label>
                    <span asp-validation-for="Input.Password" class="text-danger"></span>
                </div>
                <div class="checkbox mb-3">
                    <label asp-for="Input.RememberMe" class="form-label">
                        <input class="form-check-input" asp-for="Input.RememberMe" />
                         Remember Me
                    </label>
                </div>
                <div>
                    <button id="login-submit" type="submit" class="w-100 btn btn-lg btn-primary">Log in</button>
                </div>
                <div>
                    <p>
                        <a id="forgot-password" href="./forgotPassword">Forgot your password?</a>
                    </p>
                    <p>
                        <a href="./register" asp-route-returnUrl="{model.returnUrl}">Register as a new user</a>
                    </p>
                    <p>
                        <a id="resend-confirmation" href="./ResendEmailConfirmation">Resend email confirmation</a>
                    </p>
                </div>
            </form>
        </section>
    </div>
    <div class="col-md-6 col-md-offset-2">
        <section>
            <h3>Use another service to log in.</h3>
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
                            {#each model?.externalLogins as provider }
                                <button type="submit" class="btn btn-primary" name="provider" value="@provider.Name" title="Log in using your @provider.DisplayName account">@provider.DisplayName</button>

                            {/each}
                        </p>
                    </div>
                </form>
            {/if}
        </section>
    </div>
</div>
