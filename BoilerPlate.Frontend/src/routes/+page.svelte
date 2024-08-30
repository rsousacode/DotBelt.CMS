<script lang="ts">

    import { onMount  } from 'svelte';
    import {SITE_NAME} from "$lib/constants";
    let {data} = $props();
    
    onMount(() => {
        console.log(props);
    })
</script>


<svelte:head>
    <title>{SITE_NAME} </title>
</svelte:head>
<h1>Welcome to SvelteKit</h1>
<p>Visit <a href="https://kit.svelte.dev">kit.svelte.dev</a> to read the documentation</p>

<p>There's {data.users.length} users in your app.</p>
