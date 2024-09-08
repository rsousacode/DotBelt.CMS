<script lang="ts">
  import {onMount} from "svelte";

  let {schema, onValidSubmit} = $props();

  let form = $state();

  let errors = $state();

  function extractFormData() {
      console.log('form', form);
      const formData = new FormData(form);

      let result = {}

      getFieldNames().forEach((fieldName) => {
          result[fieldName] = formData.get(fieldName);
      })
      return result;
  }

  function getFieldNames() {
      return Object.keys(schema.fields);
  }

  async function onSubmit(e) {
      e.preventDefault();
      const isValidForm = await validateForm();

      if(isValidForm) {
          onValidSubmit(extractFormData());
      }
  }

  function resetErrors() {
      getFieldNames().forEach((fieldName) => {
          const errorForNameElement = document.querySelector(`[data-error-for="${fieldName}"]`);
          if(errorForNameElement) {
              errorForNameElement.textContent = "";
          }
      })
  }

  async function validateForm(): Promise<boolean> {
      resetErrors();
      const formData = extractFormData();
      try {
          await schema.validate(formData, { abortEarly: false });
          errors = {  };
          return true;
      } catch (err: any) {
          err.inner.forEach((error: any) => {
              const errorForNameElement = document.querySelector(`[data-error-for="${error.path}"]`);
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
      const errorForNameElement = document.querySelector(`[data-error-for="${path}"]`);

      try {
          await schema.validateAt(path, { [path]: value });
          if(errorForNameElement) {
              errorForNameElement.textContent = "";
          }
      } catch (err: any) {
          if(errorForNameElement) {
              errorForNameElement.textContent = err.message;
          }
      }
  }

  onMount(async () => {
      const elementsWithNameAttribute = document.querySelectorAll('[name]');

      elementsWithNameAttribute.forEach(el => {
          el.addEventListener('blur', async (ev) => {
              console.log('ev target', ev)
              const val = ev.target.value
              const name = ev.target.name
              await validateField(name, val);
              console.log('blur', ev)
          });
      })
  })

</script>

<form on:submit={onSubmit} bind:this={form} action="">
  <slot></slot>
</form>