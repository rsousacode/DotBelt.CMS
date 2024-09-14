<script lang="ts">
    import { toasts } from './context.svelte.js';
    import {useActions} from "$lib/Misc/Toasts/types";
    import {usePortal} from '$lib/Misc/Toasts/portal'

    import type {ToasterProps } from "$lib/Misc/Toasts/types";
    import {classProp} from "$lib/Misc/Toasts/utils.svelte";

    let {
        children,
        use = [],
        class: klass,
        self = $bindable(),
        portalTarget = 'body',
        ...props
    }: ToasterProps = $props();
</script>

<div
        bind:this={self}
        use:useActions={use}
        use:usePortal={portalTarget}
        class={classProp(klass)}
        data-toaster=""
        {...props}
>
    {@render children(toasts)}
</div>