import type {Component} from "svelte";

export type MenuItem = {
    icon: Component | undefined,
    name: string,
    href: string,
    openNewTab: boolean | undefined,
    items: MenuItem[],
}