<script lang="ts">
  import {onMount} from "svelte";

  let {schema, onValidSubmit}  = $props();

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
      getFieldNames().forEach((fieldName) => {
          cleanServerErrorElements(fieldName);
      })
      const isValidForm = await validateForm();

      if(isValidForm) {
          onValidSubmit(form, extractFormData());
      }
  }

  function extractErrorMessages(errors) {
      let errorMessages = [];
      for (let key in errors) {
          if (errors.hasOwnProperty(key)) {
              errorMessages = errorMessages.concat(errors[key]);
          }
      }
      return errorMessages;
  }

  export function onAspNetErrors(errorResult) {

      getFieldNames().forEach((fieldName) => {
          cleanServerErrorElements(fieldName);
      })


      if(getFieldNames().indexOf(errorResult.property) > -1) {

          const errorMessages = extractErrorMessages(errorResult.errors);
          addBulkErrors(errorResult.property, errorMessages);
      }
  }

  function resetErrors() {
      getFieldNames()
          .forEach(clearErrorForField);
  }

  function cleanServerErrorElements(propertyName: string) {
      // Select all elements with data-error-for="email"
      const errorElements = document.querySelectorAll(`[data-server-error-for="${propertyName}"]`);

      if (errorElements.length > 0) {
          for (let i = 0; i < errorElements.length - 1; i++) {
              errorElements[i].remove();
          }

          errorElements[errorElements.length - 1].textContent = '';
      }
  }


  function addBulkErrors(propertyName: string, errors: string[]) {
      // Select the element with data-error-for="email"
      const errorElement = document.querySelector(`[data-server-error-for="${propertyName}"]`);

      if (!errorElement) {
         return;
      } else {
          // Get the parent element
          const parentElement = errorElement.parentElement;

          if (!parentElement) {
              return;
          }
          for (let i = 0; i < errors.length; i++) {
              // Clone the element
              const clonedElement = errorElement.cloneNode(true);
              const newNode = parentElement.appendChild(clonedElement);

              // TODO: Use the code instead and get the proper i18n message?
              console.log(errors[i])
              newNode.textContent = errors[i];
          }
      }
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

      if(value === "") return;

      if(await validateForm()) {
          resetErrors();
          return;
      }
      const errorForNameElement = document.querySelector(`[data-error-for="${path}"]`);
      clearErrorForField(path);

      try {

          await schema.validateAt(path, { [path]: value });
          console.log(path + 'looks ok')
          errorForNameElement.textContent = "";
      } catch (err: any) {
          console.log('err validate field ' + path, err)
          if(errorForNameElement) {
              errorForNameElement.textContent = err.message;
          }
      }
  }

  onMount(async () => {
      const elementsWithNameAttribute = document.querySelectorAll('[name]');

      elementsWithNameAttribute.forEach(el => {
          el.addEventListener('blur', async (ev) => {
              console.log('blur', ev.target.name)
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