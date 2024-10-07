import { mount, unmount } from 'svelte';
import type { API } from '@editorjs/editorjs';

import EmbedSvelte from '$lib/Content/EditorJS/Blocks/embed/EmbedSvelte.svelte';
import type { EmbedProps } from '$lib/Content/EditorJS/Blocks/embed/EmbedProps';


type ConstructorArgs = { data: EmbedProps; };
export default class Embed {
  svelteComponent:  {$set?: never; $on?: never; } | undefined;

  data: EmbedProps = $state(
    {
      embedType: undefined,
      url: undefined
    }
  );

  api: API | undefined;

  targetElement: Element;

  constructor({ data }: ConstructorArgs) {
    this.targetElement = document.createElement('div');
    if(data.embedType === undefined) {
      this.data =  {
        embedType: undefined,
        url: undefined
      };
    } else {
      this.data = data;
    }
  }



  // noinspection JSUnusedGlobalSymbols
  static get toolbox() {
    return {
      title: 'Embed',
      icon: '<svg width="17" height="15" viewBox="0 0 336 276" xmlns="http://www.w3.org/2000/svg"><path d="M291 150V79c0-19-15-34-34-34H79c-19 0-34 15-34 34v42l67-44 81 72 56-29 42 30zm0 52l-43-30-56 30-81-67-66 39v23c0 19 15 34 34 34h178c17 0 31-13 34-29zM79 0h178c44 0 79 35 79 79v118c0 44-35 79-79 79H79c-44 0-79-35-79-79V79C0 35 35 0 79 0z"/></svg>',
    };
  }

  // noinspection JSUnusedGlobalSymbols
  render() {
    if(this.svelteComponent) {
      unmount(this.svelteComponent);
    }

    this.svelteComponent = mount(EmbedSvelte, {
      target: this.targetElement,
      props: {
        url: this.data.url,
        embedType: this.data.embedType,
        onPropsChange: (data) =>  {
          this.data = data
        }
      }
    });

    return this.targetElement;
  }

  // noinspection JSUnusedGlobalSymbols
  save() {
    console.log($state.snapshot(this.data))
    return $state.snapshot(this.data);
  }
}
