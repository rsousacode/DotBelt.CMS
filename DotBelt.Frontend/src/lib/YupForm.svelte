<script lang="ts">
  let {action, validationSchema, onValidSubmit} = $props();

  let form = $state();

  let errors = $state();

  function extractFormData() {
      const formData = new FormData(form);

      let formData = {}
      getFieldNames().forEach((fieldName) => {
          formData[fieldName] = form.get(fieldName);
      })
      return formData;
  }

  function getFieldNames() {
      return Object.keys(validationSchema.fields);
  }

  async function onSubmit(e) {
      e.preventDefault();
      const isValidForm = await validateForm();

      if(isValidForm) {
          onValidSubmit(extractFormData());
      }
  }

  async function validateForm(): Promise<boolean> {
      const formData = extractFormData();
      try {
          await validationSchema.validate(formData, { abortEarly: false });
          errors = {  };
          return true;
      } catch (err: any) {
          err.inner.forEach((error: any) => {
              const errorForNameElement = document.querySelector(`[error-for="${error.path}"]`);
              if(errorForNameElement) {
                  errorForNameElement.textContent = error.message;
              }
              //errors[error.path] = error.message;
          });
          console.log("Validation errors:", errors);
          return false;
      }
  }

  async function validateField(path: string, value: FormDataEntryValue) {
      const errorForNameElement = document.querySelector(`[error-for="${path}"]`);

      try {
          await validationSchema.validateAt(path, { [path]: value });
          errors[path] = "";
          if(errorForNameElement) {
              errorForNameElement.textContent = "";
          }
      } catch (err: any) {
          errors[path] = err.message;
          if(errorForNameElement) {
              errorForNameElement.textContent = err.message;
          }
      }
  }

</script>

<form on:submit={onSubmit} bind:this={form} action="">
  <slot></slot>
</form>