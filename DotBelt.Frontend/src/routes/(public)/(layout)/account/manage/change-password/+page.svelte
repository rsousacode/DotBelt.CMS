<script>
    import AccountManagementContainer from "$lib/Users/AccountManagementContainer.svelte";
    import {AccountManagementPages} from "$lib/Users/AccountManagementPages";
</script>

<AccountManagementContainer page={AccountManagementPages.Password}>
  <h3>Change password</h3>
  <div class="row">
    <div class="col-md-6">
      <form id="change-password-form" method="post" novalidate="novalidate">

        <div class="form-floating mb-3">
          <input class="form-control" autocomplete="current-password" aria-required="true"
                 placeholder="Please enter your old password." type="password" data-val="true"
                 data-val-required="The Current password field is required." id="Input_OldPassword"
                 name="Input.OldPassword">
          <label class="form-label" for="Input_OldPassword">Current password</label>
          <span class="text-danger field-validation-valid" data-valmsg-for="Input.OldPassword"
                data-valmsg-replace="true"></span>
        </div>
        <div class="form-floating mb-3">
          <input class="form-control" autocomplete="new-password" aria-required="true"
                 placeholder="Please enter your new password." type="password" data-val="true"
                 data-val-length="The New password must be at least 6 and at max 100 characters long."
                 data-val-length-max="100" data-val-length-min="6"
                 data-val-required="The New password field is required." id="Input_NewPassword" maxlength="100"
                 name="Input.NewPassword">
          <label class="form-label" for="Input_NewPassword">New password</label>
          <span class="text-danger field-validation-valid" data-valmsg-for="Input.NewPassword"
                data-valmsg-replace="true"></span>
        </div>
        <div class="form-floating mb-3">
          <input class="form-control" autocomplete="new-password" aria-required="true"
                 placeholder="Please confirm your new password." type="password" data-val="true"
                 data-val-equalto="The new password and confirmation password do not match."
                 data-val-equalto-other="*.NewPassword" id="Input_ConfirmPassword" name="Input.ConfirmPassword">
          <label class="form-label" for="Input_ConfirmPassword">Confirm new password</label>
          <span class="text-danger field-validation-valid" data-valmsg-for="Input.ConfirmPassword"
                data-valmsg-replace="true"></span>
        </div>
        <button type="submit" class="w-100 btn btn-lg btn-primary">Update password</button>
        <input name="__RequestVerificationToken" type="hidden"
               value="CfDJ8JgXf5XuFPxNtFFBkHzHKiE5dOdKcwCT2GR8cNKjM5e_-tr4wUUYJPx_3asbi3rovkQZpyMqpveGQyQ-K09cDKyw3lDF3EY7B9DPli96crusPZ-QLwcajiFCN5prAJ42Xr2IZABju-F8lPwm20qGT_cujzsgc8Nj8voHJjR98sg3tp6Qo2h_ymB05t2v0dKoCw">
      </form>
    </div>
  </div>
</AccountManagementContainer>


