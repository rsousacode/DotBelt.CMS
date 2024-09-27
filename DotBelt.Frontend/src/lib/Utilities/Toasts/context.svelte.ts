import type { Toast, ToastConfig, ToastType } from './types.js';
import {
  createUID,
  defaultConfig,
  parseDuration,
  type UID,
} from '$lib/Misc/Toasts/utils.svelte';
import { getContext, onDestroy } from 'svelte';

export let toasts = $state<Toast[]>([]);

/**
 * Helper function to create a svelte context with a unique name to avoid naming collisions.
 * @param name The name of the context
 */
export const setupContext = <T>() => {
  let { uid } = createUID('context');
  return {
    contextName: uid(),
    context: () => getContext<T>(uid()),
  };
};

/**
 * Auto cleanup effects when the component is unmounted from the DOM.
 */
export const effects = (fn: () => void) => {
  let cleanUp: (() => void) | null = null;

  const destroy = () => {
    if (cleanUp === null) return;
    cleanUp();
    cleanUp = null;
  };

  try {
    cleanUp = $effect.root(fn);
  } catch (error) {}

  onDestroy(destroy);

  return destroy;
};

export class Context<H = any> {
  public uid = $state<UID>()!;
  protected hooks: H | null = null;
  protected _mounted = $state<boolean>(false);

  constructor(name: string, hooks?: H) {
    const { uid } = createUID(name);
    this.uid = uid;
    if (hooks) this.hooks = hooks;
  }

  readonly #onMount = effects(() => {
    $effect(() => {
      this._mounted = true;
    });
  });
}

export class ToastContext extends Context {
  constructor() {
    super('toast');
  }
}

export class Toaster {
  add(type: ToastType, config: ToastConfig) {
    if (!config.title || !config.message)
      throw console.error('`title` and `message` must be provided.');

    const duration = parseDuration(config.duration || '5s');
    const { uid } = createUID('toast');

    const toast: Toast = {
      id: uid(),
      type,
      config: defaultConfig(config, {
        title: '',
        message: '',
        dismissable: false,
        duration,
        props: {},
      }),
    };

    toasts.push(toast);

    setTimeout(() => {
      if (toasts.find((el) => el.id === toast.id)) {
        this.removeById(toast.id);
      }
    }, duration);
  }
  removeById(toastId: string) {
    const i = toasts.findIndex((el) => el.id === toastId);
    toasts.splice(i, 1);
  }
}

export const toaster = new Toaster();
