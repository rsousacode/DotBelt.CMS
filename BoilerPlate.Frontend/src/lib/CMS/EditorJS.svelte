<script lang="ts">

    import {onMount, setContext} from "svelte";

  let { content } = $props();

  let editor : any = undefined;

  setContext('EditorJS', getData);

  export async function getData() {
      return await editor.save();
  }

  onMount(async () => {
      const EditorJS = (await import('@editorjs/editorjs')).default ;
      const List = (await import('@editorjs/list')).default;
      const Header = (await import('@editorjs/header')).default;
      const Quote = (await import('@editorjs/quote')).default;
      const InlineCode = (await import('@editorjs/inline-code')).default;
      const editorjsCodecup = (await import('@calumk/editorjs-codecup')).default;

      editor = new EditorJS({
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
              code : editorjsCodecup,
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
              quote: {
                  class: Quote,
                  inlineToolbar: true,
                  shortcut: 'CMD+SHIFT+O',
                  config: {
                      quotePlaceholder: 'Enter a quote',
                      captionPlaceholder: 'Quote\'s author',
                  },
              },
              inlineCode: {
                  class: InlineCode,
                  shortcut: 'CMD+SHIFT+M',
              },
          },
      })
  })

</script>

<div class="editor-js" id="editor-js"></div>