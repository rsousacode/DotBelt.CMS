<script lang="ts">
  import type {Component} from "svelte";
  import {fade, scale} from 'svelte/transition'
  import statefulSwapSvelte from "$lib/Utilities/Icons/statefulSwap.svelte";

  let {component1, component2}: { component1: Component, component2: Component } = $props();

  const {onOutro, transitionTo, state} = statefulSwapSvelte("first")

  export function triggerComponent2(time = 1000) {
    transitionTo("second")
    return setTimeout(() => {
      transitionTo("first")
    }, time);
  }

</script>

{#if $state === "first"}
  <div style="position: static" in:scale={{delay: 500, duration: 300}} out:fade={{duration: 300}} onoutroend={onOutro}>
    <svelte:component this={component1}/>
  </div>
{:else if $state === "second"}
  <div style="position: static" in:fade={{delay: 100, duration: 400}} out:fade={{duration: 300}} onoutroend={onOutro}>

    <svelte:component this={component2}/>
  </div>

{/if}