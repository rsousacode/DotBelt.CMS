<script lang="ts">
    import AccountManagementContainer from "$lib/Users/AccountManagementContainer.svelte";
    import {AccountManagementPages} from "$lib/Users/AccountManagementPages";
    import {page} from "$app/stores";

    import type {SessionData} from "$lib/API/GraphQL/generated";

    let session = $derived<SessionData>($page.data.session);
</script>

<AccountManagementContainer page={AccountManagementPages.Management}>
  <h3>Profile</h3>
  <div class="row">
    <div class="col-md-6">
      <form id="profile-form" method="post" novalidate="novalidate">

        <div class="form-floating mb-3">
          <input class="form-control" placeholder="Please choose your username." disabled="" type="text"
                 id="Username" name="Username" value="{session.userName}">
          <label class="form-label" for="Username">Username</label>
        </div>
        <div class="form-floating mb-3">
          <input class="form-control" placeholder="Please enter your phone number." type="tel" data-val="true"
                 data-val-phone="The Phone number field is not a valid phone number." id="Input_PhoneNumber"
                 name="Input.PhoneNumber" value="">
          <label class="form-label" for="Input_PhoneNumber">Phone number</label>
          <span class="text-danger field-validation-valid" data-valmsg-for="Input.PhoneNumber"
                data-valmsg-replace="true"></span>
        </div>
        <button id="update-profile-button" type="submit" class="w-100 btn btn-lg btn-primary">Save</button>
        <input name="__RequestVerificationToken" type="hidden"
               value="CfDJ8JgXf5XuFPxNtFFBkHzHKiE59aWSYKk3Jr2xDjpmACXcSu98w3H_izfvSnNKosmhI8F-fOrECa7gwqYlYhrfJ0WAY6e3xL3MlE_Zik4wQzO7MI7_52XfoAAon5umjk376JWMquXM_7Ox8uUlhe1vXKEuJ4PMIO1_YYSe84owaAUR0COc4R6-gT-CaQtfhhQKNg">
      </form>
    </div>
  </div>
</AccountManagementContainer>


