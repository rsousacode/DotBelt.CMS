import type {ParagraphBlockData} from "$lib/Content/EditorJS/Blocks/paragraph/ParagraphBlockData";
import type {HeadingBlockData} from "$lib/Content/EditorJS/Blocks/heading/HeadingBlockData";

type BlockData = HeadingBlockData | ParagraphBlockData ;

export type Block = {
  type: "header" | "paragraph" | "repeater"
  data: BlockData;
}


export type BlocksData = {
  blocks: Block[]
}