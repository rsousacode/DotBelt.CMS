<script lang="ts">
    import {onMount} from "svelte";

    let aceEditorElement : Element;

    let {code = $bindable()} = $props();

    onMount(async () => {
        const ace = (await import('ace-code')).default;
        let editor = ace.edit(aceEditorElement)

        // use setOptions method to set several options at once
        editor.setOptions({
            autoScrollEditorIntoView: true,
            copyWithEmptySelection: true,
        });

        let JsonMode = (await import('ace-code/src/mode/json')).Mode;

        editor.session.setMode(new JsonMode());

       editor.session.on('change', function(delta) {
           // delta.start, delta.end, delta.lines, delta.action
           code = editor.getValue();
       });

   })
</script>

<div id="editor" style="width: 600px; height: 500px; margin-top: 30px;" bind:this={aceEditorElement}>{code}</div>


