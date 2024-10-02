<script lang="ts">
  import type {ParagraphBlockData} from "$lib/Content/EditorJS/Blocks/paragraph/ParagraphBlockData";

  type Props = { block: ParagraphBlockData };
  let {block}: Props = $props();

</script>

<p>{block.text}</p>