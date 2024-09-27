import type { Snippet } from 'svelte';
export type ClassProp<C extends Record<string, any>> =
  | ((props: C) => string)
  | string
  | undefined
  | null;

// Shamelessly yoinked from https://github.com/Gibbu/Lithesome/blob/82248d432e185c9da09a80f854bf833632f519a8/src/lib/internal/helpers/action.ts#L14
// Shamelessly yoinked from https://github.com/rgossiaux/svelte-headlessui/blob/master/src/lib/hooks/use-actions.ts
// Which yoinked from https://github.com/hperrin/svelte-material-ui/blob/master/packages/common/src/internal/useActions.ts
// :)
export type SvelteSVGActionType<P> = (
  node: SVGElement,
  params?: P,
) => SvelteActionReturnType<P>;

export type SVGActionEntry<P extends any = any> =
  | SvelteSVGActionType<P>
  | [SvelteSVGActionType<P>, P];

export type SVGActionArray = SVGActionEntry[];

export type ActionArray = HTMLActionArray | SVGActionArray;

export function useActions(
  node: HTMLElement | SVGElement,
  actions: ActionArray,
) {
  let actionReturns: SvelteActionReturnType<any>[] = [];

  if (actions) {
    for (let i = 0; i < actions.length; i++) {
      const actionEntry = actions[i];
      const action = Array.isArray(actionEntry) ? actionEntry[0] : actionEntry;
      if (Array.isArray(actionEntry) && actionEntry.length > 1) {
        actionReturns.push(
          action(node as HTMLElement & SVGElement, actionEntry[1]),
        );
      } else {
        actionReturns.push(action(node as HTMLElement & SVGElement));
      }
    }
  }

  return {
    update(actions: ActionArray) {
      if (((actions && actions.length) || 0) != actionReturns.length) {
        throw new Error('You must not change the length of an actions array.');
      }

      if (actions) {
        for (let i = 0; i < actions.length; i++) {
          const returnEntry = actionReturns[i];
          if (returnEntry && returnEntry.update) {
            const actionEntry = actions[i];
            if (Array.isArray(actionEntry) && actionEntry.length > 1) {
              returnEntry.update(actionEntry[1]);
            } else {
              returnEntry.update();
            }
          }
        }
      }
    },

    destroy() {
      for (let i = 0; i < actionReturns.length; i++) {
        const returnEntry = actionReturns[i];
        if (returnEntry && returnEntry.destroy) {
          returnEntry.destroy();
        }
      }
    },
  };
}

export type SvelteActionReturnType<P> = {
  update?: (newParams?: P) => void;
  destroy?: () => void;
} | void;

export type SvelteHTMLActionType<P> = (
  node: HTMLElement,
  params?: P,
) => SvelteActionReturnType<P>;

export type HTMLActionEntry<P extends any = any> =
  | SvelteHTMLActionType<P>
  | [SvelteHTMLActionType<P>, P];

export type HTMLActionArray = HTMLActionEntry[];

export interface Props<
  T extends HTMLElement,
  C extends Record<string, any> = any,
> {
  children: Snippet<[C]>;
  /**
   * Any svelte actions you wish to pass to the underlying HTML element.
   *
   * @see https://lithesome.dev/docs/api#use-prop
   */
  use?: HTMLActionArray;
  /**
   * Any classes to be set on the underlying HTML element.
   * A function can be used to expose the component states, if any are present.
   *
   * @see https://lithesome.dev/docs/styling#class-prop-function
   */
  class?: ClassProp<C>;
  /**
   * Alias for `bind:this`, allowing for the binding of the element.
   */
  self?: T;
  [key: string]: any;
}
export type ToastType = 'success' | 'warning' | 'error' | 'info' | 'attention';

export interface ToastConfig {
  /** The title of the toast. */
  title: string;
  /** The message to be displayed. */
  message: string;
  /** The durtation of the toast to be visible. */
  duration?: string | number;
  /** Allows the toast to be removed early. */
  dismissable?: boolean;
  /** Any custom props that you wish to pass down. */
  props?: Record<string, any>;
}

export interface Toast {
  /** The unique ID ofthe toast. */
  id: string;
  /** The type of the toast. */
  type: ToastType;
  config: ToastConfig;
}

export interface ToasterProps extends Props<HTMLDivElement, Toast[]> {
  /** The element to portal the content menu to. */
  portalTarget?: string | HTMLElement;
}

export interface ToastProps extends Props<HTMLDivElement> {}

export interface ToastTitleProps extends Props<HTMLHeadingElement> {}

export interface ToastMessageProps extends Props<HTMLParagraphElement> {}

export type JsonValue =
  | null
  | boolean
  | number
  | string
  | JsonValue[]
  | JsonObject;

export type JsonObject = { [key: string]: JsonValue };
