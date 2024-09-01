import type {CodegenConfig} from '@graphql-codegen/cli'

const config: CodegenConfig = {
  schema: './schema.graphql',
/*
  documents: './src/!**!/!*.graphql',
*/
  generates: {
    './src/lib/GraphQL/generated.ts': {
      plugins: ['typescript', 'typescript-operations'],
    },
  },
}

export default config