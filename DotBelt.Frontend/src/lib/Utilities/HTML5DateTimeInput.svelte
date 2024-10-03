<script lang="ts">
  type Props = { dateTime: string };

   let {dateTime = $bindable("")} : Props = $props();

  let formatedDate: string = $derived(formatDate(new Date(dateTime)));

  function formatDate(date: Date): string {
    const pad = (num: number): string => String(num).padStart(2, '0');
    return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())}T${pad(date.getHours())}:${pad(date.getMinutes())}`;
  }

  function onInputChange(e: Event & { currentTarget: HTMLInputElement }) {
    const dateString = e.currentTarget.value;
    dateTime = new Date(dateString).toUTCString();
  }



</script>

<input value={formatedDate} onchange={onInputChange} class="form-control" type="datetime-local" />