<script lang="ts">

  import { onDestroy, onMount, setContext } from 'svelte';
  import Repeater from '$lib/Content/EditorJS/Blocks/repeater/index.svelte.js';
  import Embed from '$lib/Content/EditorJS/Blocks/embed/index.svelte';

  let { content = $bindable() } = $props();

  let editor: unknown | undefined = undefined;

  setContext('EditorJS', getData);

  export async function getData() {
    return await editor.save();
  }

  onDestroy(() => {
    if (editor) {
      editor.destroy();
    }
  });

  onMount(async () => {
    const DragDrop = (await import("editorjs-drag-drop")).default;
    const editorJsModule = (await import('@editorjs/editorjs'));

    const EditorJS = editorJsModule.default;
    const List = (await import('@editorjs/list')).default;
    const Header = (await import('@editorjs/header')).default;
    const Paragraph = (await import('@editorjs/paragraph')).default;

    const Underline = (await import('@editorjs/underline')).default;
    const Table = (await import('@editorjs/table')).default;

    console.log(Embed)


    editor = new EditorJS({
      logLevel: 'ERROR',

      onChange: async () => {
        content = JSON.stringify(await getData(), undefined, 2);
      },

      /**
       * Id of Element that should contain the Editor
       */

      holder: 'editor-js',

      data: JSON.parse(content),

      onReady: () => {
        new DragDrop(editor, "2px solid #fff")
      },

      /**
       * Available Tools list.
       * Pass Tool's class or Settings object for each Tool you want to use
       */
      tools: {
        embed: {
          class: Embed,
          config: {
            services: {
              youtube: true,
              coub: true
            }
          }
        },
        paragraph: {
          class: Paragraph,
          placeholder: 'Let`s write an awesome story!',

        },
        repeater: Repeater,
        table: Table,
        underline: Underline,
        list: {
          class: List,
          inlineToolbar: true,
          config: {
            defaultStyle: 'unordered'
          }
        },
        header: {
          inlineToolbar: true,
          class: Header,
          shortcut: 'CMD+SHIFT+H',
        },
      }
    });
  });

</script>

<svelte:head>
  <link rel="stylesheet" href="/editor-js.css" />
</svelte:head>

<div class="editor-js" id="editor-js"></div>