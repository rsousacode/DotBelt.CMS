<script lang="ts">
  import {onMount} from "svelte";
  import * as yup from "yup";

  let {schema, onValidSubmit}: Props = $props();

  type Props = { schema: yup.InferType<any>, onValidSubmit: (form: HTMLFormElement, data: any) => {} }

  let form: HTMLFormElement | undefined = $state();

  type ServerError = {property: string, errors: [code: string, error: string]}


  function extractFormData(): Record<string, any> {
    const formData = new FormData(form);

    const result: Record<string, any> = [];

    getFieldNames().forEach((fieldName : string, index) => {
      result[fieldName] = formData.get(fieldName);
    })
    return result;
  }

  function getFieldNames() {
    return Object.keys(schema.fields);
  }

  async function onSubmit(e: SubmitEvent) {
    e.preventDefault();

    if(!form) {
      return;
    }

    getFieldNames().forEach((fieldName) => {
      cleanServerErrorElements(fieldName);
    })
    const isValidForm = await validateForm();

    if (isValidForm) {
      onValidSubmit(form, extractFormData());
    }
  }

  function extractErrorMessages(errors: [code: string, error: string]) {
    let errorMessages: string[] = [];
    for (let key in errors) {
      if (errors.hasOwnProperty(key)) {
        errorMessages = errorMessages.concat(errors[key]);
      }
    }
    return errorMessages;
  }

  export function onAspNetErrors(errorResult: ServerError) {
    getFieldNames().forEach((fieldName) => {
      cleanServerErrorElements(fieldName);
    })

    if (getFieldNames().indexOf(errorResult.property) > -1) {
      const errorMessages = extractErrorMessages(errorResult.errors);
      addBulkErrors(errorResult.property, errorMessages);
    }
  }

  function resetErrors() {
    getFieldNames()
      .forEach(clearErrorForField);
  }

  function cleanServerErrorElements(propertyName: string) {

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


  async function validateForm(showErrors = true): Promise<boolean> {
    resetErrors();
    const formData = extractFormData();
    try {
      await schema.validate(formData, {abortEarly: false});
      return true;
    } catch (err: any) {
      if(showErrors) {
        err.inner.forEach((error: any) => {
          setFieldError(error.path, error.message);
        });
      }
      return false;
    }
  }

  function clearErrorForField(fieldName: string) {
    setFieldError(fieldName, "");
  }

  function setFieldError(path: string, message: string) {
    const errorForNameElement = document.querySelector(`[data-error-for="${path}"]`);
    if (errorForNameElement) {
      errorForNameElement.textContent = message;
    }
  }

  async function validateField(path: string, value: FormDataEntryValue) {

    if (value === "") return;

    if (await validateForm(false)) {
      resetErrors();
      return;
    }

    clearErrorForField(path);

    try {
      await schema.validateAt(path, {[path]: value});
      setFieldError(path, "");

    } catch (err: any) {
      setFieldError(path, err.message);
    }
  }

  onMount(async () => {
    const elementsWithNameAttribute = document.querySelectorAll('[name]');

    elementsWithNameAttribute.forEach(el => {
      el.addEventListener('blur', async (ev : Event) => {
        const target = ev.target as HTMLInputElement;

        if(target) {
          const val = target.value
          const name = target.name
          await validateField(name, val);
        }
      });
    })
  })

</script>

<form on:submit={onSubmit} bind:this={form} action="">
  <slot></slot>
</form>