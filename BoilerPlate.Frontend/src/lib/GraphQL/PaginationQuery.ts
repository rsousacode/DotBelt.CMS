import type {Maybe} from "$lib/GraphQL/generated";

export type PaginationQuery = {
    first: number| undefined,
    last: number | undefined,
    before: Maybe<string>| undefined,
    after: Maybe<string> | undefined
}