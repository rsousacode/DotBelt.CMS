
export type SupportedEmbed = "youtube" | "tiktok" | "vimeo"

export type EmbedProps = {
  embedType: SupportedEmbed | undefined,
  url: string | undefined
}

export type EmbedSvelteProps = {
  url: string | undefined,
  embedType: SupportedEmbed | undefined,
  onPropsChange: (props: EmbedProps) => void
}
