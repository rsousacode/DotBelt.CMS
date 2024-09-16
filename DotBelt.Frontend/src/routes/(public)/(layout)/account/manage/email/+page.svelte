<script lang="ts">
    import AccountManagementContainer from "$lib/Users/AccountManagementContainer.svelte";
    import {AccountManagementPages} from "$lib/Users/AccountManagementPages";
    import {page} from "$app/stores";
    import type {SessionData} from "$lib/API/GraphQL/generated";

    let session = $derived<SessionData>($page.data.session);

</script>

<AccountManagementContainer page={AccountManagementPages.Email}>

  <h3>Manage Email</h3>


  <div class="row">
    <div class="col-md-6">
      <form id="email-form" method="post" novalidate="novalidate">
        <div class="text-danger validation-summary-valid" role="alert" data-valmsg-summary="true">
          <ul>
            <li style="display:none"></li>
          </ul>
        </div>
        <div class="form-floating mb-3 input-group">
          <input class="form-control" placeholder="Please enter your email." disabled="" type="text" id="Email"
                 name="Email" value="{session.email}" wfd-id="id1">
          <div class="input-group-append">
            <span class="h-100 input-group-text text-success font-weight-bold">✓</span>
          </div>
          <label class="form-label" for="Email">Email</label>
        </div>
        <div class="form-floating mb-3">
          <input class="form-control" autocomplete="email" aria-required="true" placeholder="Please enter new email."
                 type="email" data-val="true" data-val-email="The New email field is not a valid e-mail address."
                 data-val-required="The New email field is required." id="Input_NewEmail" name="Input.NewEmail"
                 value="{session.email}" >
          <label class="form-label" for="Input_NewEmail">New email</label>
          <span class="text-danger field-validation-valid" data-valmsg-for="Input.NewEmail"
                data-valmsg-replace="true"></span>
        </div>
        <button id="change-email-button" type="submit" class="w-100 btn btn-lg btn-primary"
                formaction="/Identity/Account/Manage/Email?handler=ChangeEmail">Change email
        </button>
        <input name="__RequestVerificationToken" type="hidden"
               value="CfDJ8JgXf5XuFPxNtFFBkHzHKiGcJ35EhTnPOVAUDQzcf18z4MXrERGHIVGT1DosIe-3pluinkZ1YI8ti-s68roue91xMjncPyeqTk86C_tW-TgSv4m-n-V0ncnulk6dY5cNhNctOYRTmk39gqbahgyDFuOYQV8EsX8EMrsoznIQypXhPUboBcNPWX14wZtjqGhlvA"
              ></form>
    </div>
  </div>


</AccountManagementContainer>


