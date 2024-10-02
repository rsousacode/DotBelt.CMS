<script lang="ts">

  import {onDestroy, onMount, setContext} from "svelte";
  import Repeater from "$lib/Content/EditorJS/Blocks/repeater/index.svelte.js";

  let { content = $bindable() } = $props();

let editor : unknown | undefined = undefined;

setContext('EditorJS', getData);

export async function getData() {
  return await editor.save();
}

onDestroy(() => {
  if(editor) {
      editor.destroy();
  }
})

  onMount(async () => {
    const editorJsModule = (await import('@editorjs/editorjs'));
    const LogLevels = editorJsModule.LogLevels;
    const EditorJS = editorJsModule.default ;
    const List = (await import('@editorjs/list')).default;
    const Header = (await import('@editorjs/header')).default;
    const Underline = (await import('@editorjs/underline')).default;
    const Table = (await import('@editorjs/table')).default;


    editor = new EditorJS({
      logLevel: 'ERROR',

      onChange: async () => {
          content = JSON.stringify(await getData(), undefined, 2);
      },

      /**
       * Id of Element that should contain the Editor
       */
      placeholder: 'Let`s write an awesome story!',

      holder: 'editor-js',

      data: JSON.parse(content),

      /**
       * Available Tools list.
       * Pass Tool's class or Settings object for each Tool you want to use
       */
      tools: {
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
              class: Header,
              shortcut: 'CMD+SHIFT+H',
          },


      },
    })
  })

</script>

<svelte:head>
  <link rel="stylesheet" href="/editor-js.css" />
</svelte:head>

<div class="editor-js" id="editor-js"></div>