import { tick } from 'svelte';

/**
 * A svelte action to portal content from one part of the dom to another.
 * @param target The element to be portaled to.\
 * Default = `body`.
 */
export const usePortal = (node: HTMLElement, target: HTMLElement | string = '#layers') => {
  const update = async (newTarget: HTMLElement | string) => {
    const el: HTMLElement | null =
      typeof newTarget === 'string' ? document.querySelector(newTarget) : newTarget;

    await tick();

    if (!el) {
      console.error(`Cannot find the given target element: ${target}`);
      return;
    }

    el.appendChild(node);
  };
  const destroy = async () => {
    await tick();
    if (node) node.remove();
  };

  update(target);

  return {
    update,
    destroy,
  };
};
