import type { ClassProp, JsonValue } from '$lib/Misc/Toasts/types';

export type UID = (component?: string) => string;

/**
 * Creates a unique ID to be used for accessability.
 */
export const createUID = (namespace: string) => {
  const UID_SET = 'qwertyuiopasdfghjklzxcvbnm1234567890_-';
  const uid = '123456'
    .split('')
    .map(() => UID_SET.charAt(Math.floor(38 * Math.random())))
    .join('');

  return {
    uid: (component?: string) => {
      const id = `${namespace}-${uid}`;
      return component ? `${id}-${component}` : id;
    },
  };
};

/**
 * Merges two objects, the first being the base config, the 2nd being the defaults
 * it will fall back to if that property isn't found.
 * @param config The base object to be parsed.
 * @param defaults The object to be initalize the object.
 */
export const defaultConfig = <const T>(
  config: Partial<T> | undefined,
  defaults: Required<T>,
): Required<T> => {
  return {
    ...defaults,
    ...config,
  };
};

/**
 * Transforms the string value to a number.
 *
 * Supported identifiers: `ms`, `s`
 * @param value The value to be transformed.
 */
export const parseDuration = (value: number | string): number => {
  if (typeof value === 'number') return value;
  if (!/ms|s$/.test(value))
    throw console.error('`duration` prop was given a string but not a leading identifier (ms/s).');

  const duration: number = parseFloat(value.split(/ms|s/)[0]);

  if (/(?=ms)(?!s)/.test(value)) return duration;
  return duration * 1000;
};

/**
 * @param klass The `class` component prop.
 * @param props Any state to be passed down to the function.
 */
export const classProp = <T extends Record<string, any>>(klass: ClassProp<T>, props?: T) => {
  const _props = props || ({} as T);
  const cls = $derived(typeof klass === 'function' ? klass(_props) : klass);
  return cls;
};
