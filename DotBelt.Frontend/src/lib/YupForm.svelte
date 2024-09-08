<script lang="ts">
  import {onMount} from "svelte";

  let {schema, onValidSubmit} = $props();

  let form = $state();

  function extractFormData() {
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
          onValidSubmit(form, extractFormData());
      }
  }

  function resetErrors() {
      getFieldNames().forEach(clearErrorForField);
  }

  async function validateForm(): Promise<boolean> {
      resetErrors();
      const formData = extractFormData();
      try {
          await schema.validate(formData, { abortEarly: false });
          return true;
      } catch (err: any) {
          err.inner.forEach((error: any) => {
              const errorForNameElement = document.querySelector(`[data-error-for="${error.path}"]`);
              if(errorForNameElement) {
                  errorForNameElement.textContent = error.message;
              }
          });
          return false;
      }
  }

  function clearErrorForField(fieldName: string) {
      const errorElement = document.querySelector(`[data-error-for="${fieldName}"]`) as HTMLElement | null;
      if (errorElement) {
          errorElement.textContent = '';
      }
  }


  async function validateField(path: string, value: FormDataEntryValue) {
      const errorForNameElement = document.querySelector(`[data-error-for="${path}"]`);

      try {
          await schema.validateAt(path, { [path]: value });
          clearErrorForField(path);
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
              const val = ev.target.value
              const name = ev.target.name
              await validateField(name, val);
          });
      })
  })

</script>

<form on:submit={onSubmit} bind:this={form} action="">
  <slot></slot>
</form>