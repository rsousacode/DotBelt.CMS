import {writable} from "svelte/store";

type MenuState = {
  [key: string]: boolean | undefined;
};
const menuState = writable<MenuState>({});

export { menuState };