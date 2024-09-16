<script lang="ts">
  import {AccountManagementPages} from "$lib/Users/AccountManagementPages";

  type Props = { children: any, page: AccountManagementPages }

  let {children, page}: Props = $props();

</script>

<h1>Manage your account</h1>

<div>
  <h2>Change your account settings</h2>
  <hr>
  <div class="row">
    <div class="col-md-3">
      <ul class="nav nav-pills flex-column">
        <li class="nav-item">
          <a class="nav-link {page === AccountManagementPages.Management ? 'active' : ''}" id="profile"
             href="/account/manage">Profile</a></li>
        <li class="nav-item">
          <a class="nav-link {page === AccountManagementPages.Email ? 'active' : ''}" id="email"
             href="/account/manage/email">Email</a></li>
        <li class="nav-item">
          <a class="nav-link {page === AccountManagementPages.Password ? 'active' : ''}" id="change-password"
             href="/account/manage/change-password">Password</a>
        </li>
        <li class="nav-item">
          <a class="nav-link {page === AccountManagementPages.TwoFactorAuthentication ? 'active' : ''}" id="two-factor"
             href="/account/manage/two-factor-authentication">Two-factor authentication</a>
        </li>
        <li class="nav-item">
          <a class="nav-link {page === AccountManagementPages.PersonalData ? 'active' : ''}" id="personal-data"
             href="/account/manage/personal-data">Personal
            data</a>
        </li>
      </ul>

    </div>
    <div class="col-md-9">
      {@render children()}
    </div>
  </div>
</div>


