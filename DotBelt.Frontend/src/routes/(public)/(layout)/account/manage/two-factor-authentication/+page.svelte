<script>
    import AccountManagementContainer from "$lib/Users/AccountManagementContainer.svelte";
    import {AccountManagementPages} from "$lib/Users/AccountManagementPages";
</script>

<AccountManagementContainer page={AccountManagementPages.TwoFactorAuthentication}>



  <h3>Two-factor authentication (2FA)</h3>
  <h4>Authenticator app</h4>
  <a id="enable-authenticator" class="btn btn-primary" href="/Identity/Account/Manage/EnableAuthenticator">Set up authenticator app</a>
  <a id="reset-authenticator" class="btn btn-primary" href="/Identity/Account/Manage/ResetAuthenticator">Reset authenticator app</a>


</AccountManagementContainer>
