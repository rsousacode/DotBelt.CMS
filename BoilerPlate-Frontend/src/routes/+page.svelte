<script lang="ts">

    import { onMount  } from 'svelte';
    let {data} = $props();
    
    onMount(() => {
        console.log(props);
    })
</script>

<h1>Welcome to SvelteKit</h1>
<p>Visit <a href="https://kit.svelte.dev">kit.svelte.dev</a> to read the documentation</p>

<p>There's {data.users.length} users in your app.</p>
