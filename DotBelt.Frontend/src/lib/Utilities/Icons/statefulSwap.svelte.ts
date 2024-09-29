import { writable } from 'svelte/store';

export default function statefulSwapSvelte(initialState: string) {
  const state = writable<string | null>(initialState);
  let nextState = initialState;

  function transitionTo(newState: string) {
    if (nextState === newState) return;
    nextState = newState;
    state.set(null);
  }

  function onOutro() {
    state.set(nextState);
  }
  return {
    state,
    transitionTo,
    onOutro,
  };
}
