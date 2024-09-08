<script lang="ts">
import {SITE_NAME} from "$lib/constants";
import type {RegisterRequest} from "$lib/Swagger/generated/data-contracts";

import * as yup from 'yup';
import {onMount} from "svelte";

type RegisterForm = {
    email: FormDataEntryValue | null,
    password: FormDataEntryValue | null,
    confirmPassword: FormDataEntryValue | null
}

type RegisterFormError = {
    email: string,
    password: string,
    confirmPassword: string,
}

async function validateField(path: string, value: FormDataEntryValue) {
    try {
        await validationSchema.validateAt(path, { [path]: value });
        errors[path] = "";
    } catch (err: any) {
        errors[path] = err.message;
    }
}

let errors : RegisterFormError = $state({ email: "", password: "", confirmPassword: "" });

let form;

const validationSchema = yup.object().shape({
    email: yup.string().email('Invalid email address').required('Email is required'),
    password: yup.string().required('Password is required'),
    confirmPassword: yup.string().oneOf([yup.ref('password'), null], 'Passwords must match').required('Confirm password is required')
});

async function validateForm(event: Event): Promise<boolean> {
    const formData = extractFormData(event);
    try {
        await validationSchema.validate(formData, { abortEarly: false });
        console.log("Form is valid");
        errors = { email: "", password: "", confirmPassword: "" };
        return true;
    } catch (err: any) {
        err.inner.forEach((error: any) => {
            errors[error.path] = error.message;
        });
        console.log("Validation errors:", errors);
        return false;
    }
}
function extractFormData(event: Event): RegisterForm {
    const formData = new FormData(event.target as HTMLFormElement);
    return {
        email: formData.get('Input.Email'),
        password: formData.get('Input.Password'),
        confirmPassword: formData.get('Input.ConfirmPassword'),
    };
}

async function handleSubmit(event: SubmitEvent) {
    event.preventDefault();
    if(!event.target) return;

    const isValid = await validateForm(event);
    if (isValid) {
        // handle successful form submission
    }
}

onMount(() => {
    console.log(validationSchema);
})
</script>

<svelte:head>
    <title>Register - {SITE_NAME} </title>
</svelte:head>

<h1>Register</h1>

<div class="row">
    <div class="col-md-4">
        <form bind:this={form}  on:submit={handleSubmit} id="registerForm" method="post">
            <h2>Create a new account.</h2>
            <hr />
            <div class="text-danger" role="alert"></div>
            <div class="form-floating mb-3">
                <input on:blur="{(e) => validateField('email', e.target.value)}" name="Input.Email" class="form-control" autocomplete="username" aria-required="true" placeholder="name@example.com" />
                <label  for="Input.Email">Email</label>
                <span  class="text-danger">{errors.email}</span>
            </div>
            <div class="form-floating mb-3">
                <input on:blur="{(e) => validateField('password', e.target.value)}" name="Input.Password" class="form-control" autocomplete="new-password" aria-required="true" placeholder="password" />
                <label for="Input.Password">Password</label>
                <span class="text-danger">{errors.password}</span>
            </div>
            <div class="form-floating mb-3">
                <input on:blur="{(e) => validateField('confirmPassword', e.target.value)}" name="Input.ConfirmPassword" class="form-control" autocomplete="new-password" aria-required="true" placeholder="password" />
                <label for="Input.ConfirmPassword">Confirm Password</label>
                <span  class="text-danger">{errors.confirmPassword}</span>
            </div>
            <button id="registerSubmit" type="submit" class="w-100 btn btn-lg btn-primary">Register</button>
        </form>
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