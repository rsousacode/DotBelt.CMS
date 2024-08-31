export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: { input: any; output: any; }
};

export type ApplicationUser = {
  __typename?: 'ApplicationUser';
  accessFailedCount: Scalars['Int']['output'];
  concurrencyStamp?: Maybe<Scalars['String']['output']>;
  email?: Maybe<Scalars['String']['output']>;
  emailConfirmed: Scalars['Boolean']['output'];
  id: Scalars['String']['output'];
  lockoutEnabled: Scalars['Boolean']['output'];
  lockoutEnd?: Maybe<Scalars['DateTime']['output']>;
  normalizedEmail?: Maybe<Scalars['String']['output']>;
  normalizedUserName?: Maybe<Scalars['String']['output']>;
  passwordHash?: Maybe<Scalars['String']['output']>;
  phoneNumber?: Maybe<Scalars['String']['output']>;
  phoneNumberConfirmed: Scalars['Boolean']['output'];
  securityStamp?: Maybe<Scalars['String']['output']>;
  twoFactorEnabled: Scalars['Boolean']['output'];
  userName?: Maybe<Scalars['String']['output']>;
};

export type ApplicationUserFilterInput = {
  accessFailedCount?: InputMaybe<IntOperationFilterInput>;
  and?: InputMaybe<Array<ApplicationUserFilterInput>>;
  concurrencyStamp?: InputMaybe<StringOperationFilterInput>;
  email?: InputMaybe<StringOperationFilterInput>;
  emailConfirmed?: InputMaybe<BooleanOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  lockoutEnabled?: InputMaybe<BooleanOperationFilterInput>;
  lockoutEnd?: InputMaybe<DateTimeOperationFilterInput>;
  normalizedEmail?: InputMaybe<StringOperationFilterInput>;
  normalizedUserName?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ApplicationUserFilterInput>>;
  passwordHash?: InputMaybe<StringOperationFilterInput>;
  phoneNumber?: InputMaybe<StringOperationFilterInput>;
  phoneNumberConfirmed?: InputMaybe<BooleanOperationFilterInput>;
  securityStamp?: InputMaybe<StringOperationFilterInput>;
  twoFactorEnabled?: InputMaybe<BooleanOperationFilterInput>;
  userName?: InputMaybe<StringOperationFilterInput>;
};

export type ApplicationUserSortInput = {
  accessFailedCount?: InputMaybe<SortEnumType>;
  concurrencyStamp?: InputMaybe<SortEnumType>;
  email?: InputMaybe<SortEnumType>;
  emailConfirmed?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  lockoutEnabled?: InputMaybe<SortEnumType>;
  lockoutEnd?: InputMaybe<SortEnumType>;
  normalizedEmail?: InputMaybe<SortEnumType>;
  normalizedUserName?: InputMaybe<SortEnumType>;
  passwordHash?: InputMaybe<SortEnumType>;
  phoneNumber?: InputMaybe<SortEnumType>;
  phoneNumberConfirmed?: InputMaybe<SortEnumType>;
  securityStamp?: InputMaybe<SortEnumType>;
  twoFactorEnabled?: InputMaybe<SortEnumType>;
  userName?: InputMaybe<SortEnumType>;
};

export type BooleanOperationFilterInput = {
  eq?: InputMaybe<Scalars['Boolean']['input']>;
  neq?: InputMaybe<Scalars['Boolean']['input']>;
};

export type CreatePostInput = {
  content?: InputMaybe<Scalars['String']['input']>;
  description: Scalars['String']['input'];
  title?: InputMaybe<Scalars['String']['input']>;
  urlName?: InputMaybe<Scalars['String']['input']>;
};

export type CreatePostPayload = {
  __typename?: 'CreatePostPayload';
  post?: Maybe<Post>;
};

export type DateTimeOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']['input']>;
  gt?: InputMaybe<Scalars['DateTime']['input']>;
  gte?: InputMaybe<Scalars['DateTime']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['DateTime']['input']>>>;
  lt?: InputMaybe<Scalars['DateTime']['input']>;
  lte?: InputMaybe<Scalars['DateTime']['input']>;
  neq?: InputMaybe<Scalars['DateTime']['input']>;
  ngt?: InputMaybe<Scalars['DateTime']['input']>;
  ngte?: InputMaybe<Scalars['DateTime']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['DateTime']['input']>>>;
  nlt?: InputMaybe<Scalars['DateTime']['input']>;
  nlte?: InputMaybe<Scalars['DateTime']['input']>;
};

export type GraphQlMutation = {
  __typename?: 'GraphQLMutation';
  createPost: CreatePostPayload;
};


export type GraphQlMutationCreatePostArgs = {
  payload: CreatePostInput;
};

export type GraphQlQuery = {
  __typename?: 'GraphQLQuery';
  postById: Array<Post>;
  posts?: Maybe<PostsConnection>;
  taxonomies?: Maybe<TaxonomiesConnection>;
  taxonomyById: Array<Taxonomy>;
};


export type GraphQlQueryPostByIdArgs = {
  id: Scalars['Int']['input'];
};


export type GraphQlQueryPostsArgs = {
  after?: InputMaybe<Scalars['String']['input']>;
  before?: InputMaybe<Scalars['String']['input']>;
  first?: InputMaybe<Scalars['Int']['input']>;
  last?: InputMaybe<Scalars['Int']['input']>;
  order?: InputMaybe<Array<PostSortInput>>;
  where?: InputMaybe<PostFilterInput>;
};


export type GraphQlQueryTaxonomiesArgs = {
  after?: InputMaybe<Scalars['String']['input']>;
  before?: InputMaybe<Scalars['String']['input']>;
  first?: InputMaybe<Scalars['Int']['input']>;
  last?: InputMaybe<Scalars['Int']['input']>;
  order?: InputMaybe<Array<TaxonomySortInput>>;
  where?: InputMaybe<TaxonomyFilterInput>;
};


export type GraphQlQueryTaxonomyByIdArgs = {
  id: Scalars['Int']['input'];
};

export type IntOperationFilterInput = {
  eq?: InputMaybe<Scalars['Int']['input']>;
  gt?: InputMaybe<Scalars['Int']['input']>;
  gte?: InputMaybe<Scalars['Int']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Int']['input']>>>;
  lt?: InputMaybe<Scalars['Int']['input']>;
  lte?: InputMaybe<Scalars['Int']['input']>;
  neq?: InputMaybe<Scalars['Int']['input']>;
  ngt?: InputMaybe<Scalars['Int']['input']>;
  ngte?: InputMaybe<Scalars['Int']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Int']['input']>>>;
  nlt?: InputMaybe<Scalars['Int']['input']>;
  nlte?: InputMaybe<Scalars['Int']['input']>;
};

export type ListFilterInputTypeOfPostFilterInput = {
  all?: InputMaybe<PostFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<PostFilterInput>;
  some?: InputMaybe<PostFilterInput>;
};

export type ListFilterInputTypeOfTaxonomyFilterInput = {
  all?: InputMaybe<TaxonomyFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<TaxonomyFilterInput>;
  some?: InputMaybe<TaxonomyFilterInput>;
};

/** Information about pagination in a connection. */
export type PageInfo = {
  __typename?: 'PageInfo';
  /** When paginating forwards, the cursor to continue. */
  endCursor?: Maybe<Scalars['String']['output']>;
  /** Indicates whether more edges exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean']['output'];
  /** Indicates whether more edges exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean']['output'];
  /** When paginating backwards, the cursor to continue. */
  startCursor?: Maybe<Scalars['String']['output']>;
};

export type Post = {
  __typename?: 'Post';
  author?: Maybe<ApplicationUser>;
  authorId?: Maybe<Scalars['String']['output']>;
  childrenPosts: Array<Post>;
  content?: Maybe<Scalars['String']['output']>;
  contentHtml?: Maybe<Scalars['String']['output']>;
  description: Scalars['String']['output'];
  id: Scalars['Int']['output'];
  modifiedDate?: Maybe<Scalars['DateTime']['output']>;
  parentPost?: Maybe<Post>;
  postType: PostTypeEnum;
  publishDate: Scalars['DateTime']['output'];
  taxonomies: Array<Taxonomy>;
  title?: Maybe<Scalars['String']['output']>;
  urlName?: Maybe<Scalars['String']['output']>;
};

export type PostFilterInput = {
  and?: InputMaybe<Array<PostFilterInput>>;
  author?: InputMaybe<ApplicationUserFilterInput>;
  authorId?: InputMaybe<StringOperationFilterInput>;
  childrenPosts?: InputMaybe<ListFilterInputTypeOfPostFilterInput>;
  content?: InputMaybe<StringOperationFilterInput>;
  contentHtml?: InputMaybe<StringOperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<IntOperationFilterInput>;
  modifiedDate?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<PostFilterInput>>;
  parentPost?: InputMaybe<PostFilterInput>;
  postType?: InputMaybe<PostTypeEnumOperationFilterInput>;
  publishDate?: InputMaybe<DateTimeOperationFilterInput>;
  taxonomies?: InputMaybe<ListFilterInputTypeOfTaxonomyFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
  urlName?: InputMaybe<StringOperationFilterInput>;
};

export type PostSortInput = {
  author?: InputMaybe<ApplicationUserSortInput>;
  authorId?: InputMaybe<SortEnumType>;
  content?: InputMaybe<SortEnumType>;
  contentHtml?: InputMaybe<SortEnumType>;
  description?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  modifiedDate?: InputMaybe<SortEnumType>;
  parentPost?: InputMaybe<PostSortInput>;
  postType?: InputMaybe<SortEnumType>;
  publishDate?: InputMaybe<SortEnumType>;
  title?: InputMaybe<SortEnumType>;
  urlName?: InputMaybe<SortEnumType>;
};

export enum PostTypeEnum {
  Page = 'PAGE',
  Post = 'POST',
  Undefined = 'UNDEFINED'
}

export type PostTypeEnumOperationFilterInput = {
  eq?: InputMaybe<PostTypeEnum>;
  in?: InputMaybe<Array<PostTypeEnum>>;
  neq?: InputMaybe<PostTypeEnum>;
  nin?: InputMaybe<Array<PostTypeEnum>>;
};

/** A connection to a list of items. */
export type PostsConnection = {
  __typename?: 'PostsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<PostsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Post>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
};

/** An edge in a connection. */
export type PostsEdge = {
  __typename?: 'PostsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String']['output'];
  /** The item at the end of the edge. */
  node: Post;
};

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC'
}

export type StringOperationFilterInput = {
  and?: InputMaybe<Array<StringOperationFilterInput>>;
  contains?: InputMaybe<Scalars['String']['input']>;
  endsWith?: InputMaybe<Scalars['String']['input']>;
  eq?: InputMaybe<Scalars['String']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  ncontains?: InputMaybe<Scalars['String']['input']>;
  nendsWith?: InputMaybe<Scalars['String']['input']>;
  neq?: InputMaybe<Scalars['String']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  nstartsWith?: InputMaybe<Scalars['String']['input']>;
  or?: InputMaybe<Array<StringOperationFilterInput>>;
  startsWith?: InputMaybe<Scalars['String']['input']>;
};

/** A connection to a list of items. */
export type TaxonomiesConnection = {
  __typename?: 'TaxonomiesConnection';
  /** A list of edges. */
  edges?: Maybe<Array<TaxonomiesEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Taxonomy>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
};

/** An edge in a connection. */
export type TaxonomiesEdge = {
  __typename?: 'TaxonomiesEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String']['output'];
  /** The item at the end of the edge. */
  node: Taxonomy;
};

export type Taxonomy = {
  __typename?: 'Taxonomy';
  author?: Maybe<ApplicationUser>;
  authorId?: Maybe<Scalars['String']['output']>;
  childrenTaxonomies: Array<Taxonomy>;
  description: Scalars['String']['output'];
  id: Scalars['Int']['output'];
  modifiedDate?: Maybe<Scalars['DateTime']['output']>;
  parentTaxonomy?: Maybe<Taxonomy>;
  posts: Array<Post>;
  publishDate: Scalars['DateTime']['output'];
  title: Scalars['String']['output'];
  type: TaxonomyTypeEnum;
  urlName: Scalars['String']['output'];
};

export type TaxonomyFilterInput = {
  and?: InputMaybe<Array<TaxonomyFilterInput>>;
  author?: InputMaybe<ApplicationUserFilterInput>;
  authorId?: InputMaybe<StringOperationFilterInput>;
  childrenTaxonomies?: InputMaybe<ListFilterInputTypeOfTaxonomyFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<IntOperationFilterInput>;
  modifiedDate?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<TaxonomyFilterInput>>;
  parentTaxonomy?: InputMaybe<TaxonomyFilterInput>;
  posts?: InputMaybe<ListFilterInputTypeOfPostFilterInput>;
  publishDate?: InputMaybe<DateTimeOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
  type?: InputMaybe<TaxonomyTypeEnumOperationFilterInput>;
  urlName?: InputMaybe<StringOperationFilterInput>;
};

export type TaxonomySortInput = {
  author?: InputMaybe<ApplicationUserSortInput>;
  authorId?: InputMaybe<SortEnumType>;
  description?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  modifiedDate?: InputMaybe<SortEnumType>;
  parentTaxonomy?: InputMaybe<TaxonomySortInput>;
  publishDate?: InputMaybe<SortEnumType>;
  title?: InputMaybe<SortEnumType>;
  type?: InputMaybe<SortEnumType>;
  urlName?: InputMaybe<SortEnumType>;
};

export enum TaxonomyTypeEnum {
  Category = 'CATEGORY',
  Tag = 'TAG',
  Undefined = 'UNDEFINED'
}

export type TaxonomyTypeEnumOperationFilterInput = {
  eq?: InputMaybe<TaxonomyTypeEnum>;
  in?: InputMaybe<Array<TaxonomyTypeEnum>>;
  neq?: InputMaybe<TaxonomyTypeEnum>;
  nin?: InputMaybe<Array<TaxonomyTypeEnum>>;
};
