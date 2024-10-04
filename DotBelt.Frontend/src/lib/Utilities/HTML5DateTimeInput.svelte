<script lang="ts">
  type Props = { dateTime: string, onchange?: (e?: OnChangeEvent) => void };

  type OnChangeEvent = Event & { currentTarget: HTMLInputElement };

   let {dateTime = $bindable(""), onchange} : Props = $props();

  let formatedDate: string = $derived(formatDate(new Date(dateTime)));

  function formatDate(date: Date): string {
    const pad = (num: number): string => String(num).padStart(2, '0');
    return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())}T${pad(date.getHours())}:${pad(date.getMinutes())}`;
  }

  function onInputChange(e: OnChangeEvent) {
    const dateString = e.currentTarget.value;
    dateTime = new Date(dateString).toUTCString();
    if(onchange !== undefined) {
      onchange(e);
    }

  }



</script>

<input value={formatedDate} onchange={onInputChange} class="form-control" type="datetime-local" />