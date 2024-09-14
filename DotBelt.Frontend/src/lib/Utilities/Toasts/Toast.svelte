<script lang="ts" context="module">
    import {setupContext, ToastContext} from "$lib/Misc/Toasts/context.svelte";

    export const { context, contextName } = setupContext<ToastContext>();
</script>

<script lang="ts">
    import { useActions } from "$lib/Misc/Toasts/types";
    import type { ToastProps } from './types.js';
    import { setContext } from 'svelte';
    import {classProp} from "$lib/Misc/Toasts/utils.svelte";

    let { children, use = [], class: klass, self = $bindable(), ...props }: ToastProps = $props();

    const ctx = new ToastContext();

    setContext(contextName, ctx);
</script>

<div
        bind:this={self}
        use:useActions={use}
        class={classProp(klass)}
        role="alert"
        aria-labelledby={ctx.uid('title')}
        aria-describedby={ctx.uid('description')}
        aria-live="assertive"
        tabindex="-1"
        data-toast=""
        {...props}
>
    {@render children({})}
</div>