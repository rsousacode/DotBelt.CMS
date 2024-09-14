<script lang="ts">
    import { context } from './Toast.svelte';
    import {classProp} from "$lib/Misc/Toasts/utils.svelte";

    import {type ToastMessageProps, useActions} from "$lib/Misc/Toasts/types";

    let { children, use = [], class: klass, self = $bindable(), ...props }: ToastMessageProps = $props();

    const ctx = context();
</script>

<p
        bind:this={self}
        use:useActions={use}
        id={ctx.uid('description')}
        class={classProp(klass)}
        data-toastdescription=""
        {...props}
>
    {@render children({})}
</p>