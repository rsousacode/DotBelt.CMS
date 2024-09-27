export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = {
  [K in keyof T]: T[K];
};
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & {
  [SubKey in K]?: Maybe<T[SubKey]>;
};
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & {
  [SubKey in K]: Maybe<T[SubKey]>;
};
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = {
  [_ in K]?: never;
};
export type Incremental<T> =
  | T
  | {
      [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never;
    };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string };
  String: { input: string; output: string };
  Boolean: { input: boolean; output: boolean };
  Int: { input: number; output: number };
  Float: { input: number; output: number };
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: { input: any; output: any };
};

export type ApplicationUser = {
  __typename?: 'ApplicationUser';
  accessFailedCount: Scalars['Int']['output'];
  concurrencyStamp?: Maybe<Scalars['String']['output']>;
  email?: Maybe<Scalars['String']['output']>;
  emailConfirmed: Scalars['Boolean']['output'];
  id: Scalars['Int']['output'];
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
  id?: InputMaybe<IntOperationFilterInput>;
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

export enum ApplyPolicy {
  AfterResolver = 'AFTER_RESOLVER',
  BeforeResolver = 'BEFORE_RESOLVER',
  Validation = 'VALIDATION',
}

export type BooleanOperationFilterInput = {
  eq?: InputMaybe<Scalars['Boolean']['input']>;
  neq?: InputMaybe<Scalars['Boolean']['input']>;
};

export type CreatePostInput = {
  payload: Create_PostRequestInput;
  type: PostTypeEnum;
};

export type CreatePostPayload = {
  __typename?: 'CreatePostPayload';
  post?: Maybe<Post>;
};

export type Create_PostRequestInput = {
  content: Scalars['String']['input'];
  description?: InputMaybe<Scalars['String']['input']>;
  relativeUrl?: InputMaybe<Scalars['String']['input']>;
  title?: InputMaybe<Scalars['String']['input']>;
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

export type DeleteUploadsInput = {
  uploadIds: Array<Scalars['Int']['input']>;
};

export type DeleteUploadsPayload = {
  __typename?: 'DeleteUploadsPayload';
  removeUploadsResult?: Maybe<RemoveUploadsResult>;
};

export type EditPostInput = {
  payload: Edit_PostRequestInput;
  postId: Scalars['Int']['input'];
};

export type EditPostPayload = {
  __typename?: 'EditPostPayload';
  editPostResult?: Maybe<EditPostResult>;
};

export type EditPostResult = {
  __typename?: 'EditPostResult';
  success: Scalars['Boolean']['output'];
};

export type Edit_PostRequestInput = {
  content: Scalars['String']['input'];
  description?: InputMaybe<Scalars['String']['input']>;
  relativeUrl?: InputMaybe<Scalars['String']['input']>;
  title?: InputMaybe<Scalars['String']['input']>;
};

export type GraphQlMutation = {
  __typename?: 'GraphQLMutation';
  createPost: CreatePostPayload;
  deleteUploads: DeleteUploadsPayload;
  editPost: EditPostPayload;
};

export type GraphQlMutationCreatePostArgs = {
  input: CreatePostInput;
};

export type GraphQlMutationDeleteUploadsArgs = {
  input: DeleteUploadsInput;
};

export type GraphQlMutationEditPostArgs = {
  input: EditPostInput;
};

export type GraphQlQuery = {
  __typename?: 'GraphQLQuery';
  postById: Array<PostResponse>;
  postByUrl: Array<PostResponse>;
  posts?: Maybe<PostsConnection>;
  session: SessionData;
  taxonomies?: Maybe<TaxonomiesConnection>;
  taxonomyById: Array<Taxonomy>;
  uploads?: Maybe<UploadsConnection>;
  users?: Maybe<UsersConnection>;
};

export type GraphQlQueryPostByIdArgs = {
  id: Scalars['Int']['input'];
};

export type GraphQlQueryPostByUrlArgs = {
  url: Scalars['String']['input'];
};

export type GraphQlQueryPostsArgs = {
  after?: InputMaybe<Scalars['String']['input']>;
  before?: InputMaybe<Scalars['String']['input']>;
  first?: InputMaybe<Scalars['Int']['input']>;
  last?: InputMaybe<Scalars['Int']['input']>;
  order?: InputMaybe<Array<PostResponseSortInput>>;
  where?: InputMaybe<PostResponseFilterInput>;
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

export type GraphQlQueryUploadsArgs = {
  after?: InputMaybe<Scalars['String']['input']>;
  before?: InputMaybe<Scalars['String']['input']>;
  first?: InputMaybe<Scalars['Int']['input']>;
  last?: InputMaybe<Scalars['Int']['input']>;
  order?: InputMaybe<Array<UploadResponseSortInput>>;
  where?: InputMaybe<UploadResponseFilterInput>;
};

export type GraphQlQueryUsersArgs = {
  after?: InputMaybe<Scalars['String']['input']>;
  before?: InputMaybe<Scalars['String']['input']>;
  first?: InputMaybe<Scalars['Int']['input']>;
  last?: InputMaybe<Scalars['Int']['input']>;
  order?: InputMaybe<Array<UserResponseSortInput>>;
  where?: InputMaybe<UserResponseFilterInput>;
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

export type ListFilterInputTypeOfTaxonomyResponseFilterInput = {
  all?: InputMaybe<TaxonomyResponseFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<TaxonomyResponseFilterInput>;
  some?: InputMaybe<TaxonomyResponseFilterInput>;
};

export type ListFilterInputTypeOfUploadFilterInput = {
  all?: InputMaybe<UploadFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<UploadFilterInput>;
  some?: InputMaybe<UploadFilterInput>;
};

export type ListStringOperationFilterInput = {
  all?: InputMaybe<StringOperationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<StringOperationFilterInput>;
  some?: InputMaybe<StringOperationFilterInput>;
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
  author: ApplicationUser;
  authorId: Scalars['Int']['output'];
  childrenPosts: Array<Post>;
  content?: Maybe<Scalars['String']['output']>;
  contentHtml?: Maybe<Scalars['String']['output']>;
  description?: Maybe<Scalars['String']['output']>;
  featuredImage?: Maybe<Upload>;
  featuredImageId?: Maybe<Scalars['Int']['output']>;
  fullUrl?: Maybe<Scalars['String']['output']>;
  id: Scalars['Int']['output'];
  inTrash?: Maybe<Scalars['Boolean']['output']>;
  modifiedDate?: Maybe<Scalars['DateTime']['output']>;
  parentPost?: Maybe<Post>;
  parentPostId?: Maybe<Scalars['Int']['output']>;
  postType: PostTypeEnum;
  publishDate: Scalars['DateTime']['output'];
  relativeUrl: Scalars['String']['output'];
  taxonomies: Array<Taxonomy>;
  tenant: Tenant;
  tenantId: Scalars['Int']['output'];
  title?: Maybe<Scalars['String']['output']>;
};

export type PostFilterInput = {
  and?: InputMaybe<Array<PostFilterInput>>;
  author?: InputMaybe<ApplicationUserFilterInput>;
  authorId?: InputMaybe<IntOperationFilterInput>;
  childrenPosts?: InputMaybe<ListFilterInputTypeOfPostFilterInput>;
  content?: InputMaybe<StringOperationFilterInput>;
  contentHtml?: InputMaybe<StringOperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  featuredImage?: InputMaybe<UploadFilterInput>;
  featuredImageId?: InputMaybe<IntOperationFilterInput>;
  fullUrl?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<IntOperationFilterInput>;
  inTrash?: InputMaybe<BooleanOperationFilterInput>;
  modifiedDate?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<PostFilterInput>>;
  parentPost?: InputMaybe<PostFilterInput>;
  parentPostId?: InputMaybe<IntOperationFilterInput>;
  postType?: InputMaybe<PostTypeEnumOperationFilterInput>;
  publishDate?: InputMaybe<DateTimeOperationFilterInput>;
  relativeUrl?: InputMaybe<StringOperationFilterInput>;
  taxonomies?: InputMaybe<ListFilterInputTypeOfTaxonomyFilterInput>;
  tenant?: InputMaybe<TenantFilterInput>;
  tenantId?: InputMaybe<IntOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
};

export type PostResponse = {
  __typename?: 'PostResponse';
  author?: Maybe<UserResponse>;
  authorId?: Maybe<Scalars['Int']['output']>;
  content?: Maybe<Scalars['String']['output']>;
  contentHtml?: Maybe<Scalars['String']['output']>;
  description?: Maybe<Scalars['String']['output']>;
  fullUrl?: Maybe<Scalars['String']['output']>;
  id: Scalars['Int']['output'];
  modifiedDate?: Maybe<Scalars['DateTime']['output']>;
  parentPostId?: Maybe<Scalars['Int']['output']>;
  postType: PostTypeEnum;
  publishDate: Scalars['DateTime']['output'];
  relativeUrl: Scalars['String']['output'];
  taxonomies: Array<TaxonomyResponse>;
  title?: Maybe<Scalars['String']['output']>;
};

export type PostResponseFilterInput = {
  and?: InputMaybe<Array<PostResponseFilterInput>>;
  author?: InputMaybe<UserResponseFilterInput>;
  authorId?: InputMaybe<IntOperationFilterInput>;
  content?: InputMaybe<StringOperationFilterInput>;
  contentHtml?: InputMaybe<StringOperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  fullUrl?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<IntOperationFilterInput>;
  modifiedDate?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<PostResponseFilterInput>>;
  parentPostId?: InputMaybe<IntOperationFilterInput>;
  postType?: InputMaybe<PostTypeEnumOperationFilterInput>;
  publishDate?: InputMaybe<DateTimeOperationFilterInput>;
  relativeUrl?: InputMaybe<StringOperationFilterInput>;
  taxonomies?: InputMaybe<ListFilterInputTypeOfTaxonomyResponseFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
};

export type PostResponseSortInput = {
  author?: InputMaybe<UserResponseSortInput>;
  authorId?: InputMaybe<SortEnumType>;
  content?: InputMaybe<SortEnumType>;
  contentHtml?: InputMaybe<SortEnumType>;
  description?: InputMaybe<SortEnumType>;
  fullUrl?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  modifiedDate?: InputMaybe<SortEnumType>;
  parentPostId?: InputMaybe<SortEnumType>;
  postType?: InputMaybe<SortEnumType>;
  publishDate?: InputMaybe<SortEnumType>;
  relativeUrl?: InputMaybe<SortEnumType>;
  title?: InputMaybe<SortEnumType>;
};

export enum PostTypeEnum {
  MenuItem = 'MENU_ITEM',
  Page = 'PAGE',
  Post = 'POST',
  Undefined = 'UNDEFINED',
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
  nodes?: Maybe<Array<PostResponse>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** Identifies the total count of items in the connection. */
  totalCount: Scalars['Int']['output'];
};

/** An edge in a connection. */
export type PostsEdge = {
  __typename?: 'PostsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String']['output'];
  /** The item at the end of the edge. */
  node: PostResponse;
};

export type RemoveUploadsResult = {
  __typename?: 'RemoveUploadsResult';
  deletedIds?: Maybe<Array<Scalars['Int']['output']>>;
};

export type SessionData = {
  __typename?: 'SessionData';
  email?: Maybe<Scalars['String']['output']>;
  id?: Maybe<Scalars['Int']['output']>;
  isAuthenticated: Scalars['Boolean']['output'];
  userName?: Maybe<Scalars['String']['output']>;
};

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC',
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
  author: ApplicationUser;
  authorId: Scalars['Int']['output'];
  description?: Maybe<Scalars['String']['output']>;
  fullUrl?: Maybe<Scalars['String']['output']>;
  id: Scalars['Int']['output'];
  inTrash?: Maybe<Scalars['Boolean']['output']>;
  modifiedDate?: Maybe<Scalars['DateTime']['output']>;
  parentTaxonomyId?: Maybe<Scalars['Int']['output']>;
  posts: Array<Post>;
  publishDate: Scalars['DateTime']['output'];
  relativeUrl: Scalars['String']['output'];
  tenant: Tenant;
  tenantId: Scalars['Int']['output'];
  title?: Maybe<Scalars['String']['output']>;
  type: TaxonomyTypeEnum;
};

export type TaxonomyFilterInput = {
  and?: InputMaybe<Array<TaxonomyFilterInput>>;
  author?: InputMaybe<ApplicationUserFilterInput>;
  authorId?: InputMaybe<IntOperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  fullUrl?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<IntOperationFilterInput>;
  inTrash?: InputMaybe<BooleanOperationFilterInput>;
  modifiedDate?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<TaxonomyFilterInput>>;
  parentTaxonomyId?: InputMaybe<IntOperationFilterInput>;
  posts?: InputMaybe<ListFilterInputTypeOfPostFilterInput>;
  publishDate?: InputMaybe<DateTimeOperationFilterInput>;
  relativeUrl?: InputMaybe<StringOperationFilterInput>;
  tenant?: InputMaybe<TenantFilterInput>;
  tenantId?: InputMaybe<IntOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
  type?: InputMaybe<TaxonomyTypeEnumOperationFilterInput>;
};

export type TaxonomyResponse = {
  __typename?: 'TaxonomyResponse';
  author?: Maybe<UserResponse>;
  authorId?: Maybe<Scalars['Int']['output']>;
  description?: Maybe<Scalars['String']['output']>;
  id: Scalars['Int']['output'];
  modifiedDate?: Maybe<Scalars['DateTime']['output']>;
  parentTaxonomyId?: Maybe<Scalars['Int']['output']>;
  publishDate: Scalars['DateTime']['output'];
  relativeUrl: Scalars['String']['output'];
  title?: Maybe<Scalars['String']['output']>;
  type: TaxonomyTypeEnum;
};

export type TaxonomyResponseFilterInput = {
  and?: InputMaybe<Array<TaxonomyResponseFilterInput>>;
  author?: InputMaybe<UserResponseFilterInput>;
  authorId?: InputMaybe<IntOperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<IntOperationFilterInput>;
  modifiedDate?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<TaxonomyResponseFilterInput>>;
  parentTaxonomyId?: InputMaybe<IntOperationFilterInput>;
  publishDate?: InputMaybe<DateTimeOperationFilterInput>;
  relativeUrl?: InputMaybe<StringOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
  type?: InputMaybe<TaxonomyTypeEnumOperationFilterInput>;
};

export type TaxonomySortInput = {
  author?: InputMaybe<ApplicationUserSortInput>;
  authorId?: InputMaybe<SortEnumType>;
  description?: InputMaybe<SortEnumType>;
  fullUrl?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  inTrash?: InputMaybe<SortEnumType>;
  modifiedDate?: InputMaybe<SortEnumType>;
  parentTaxonomyId?: InputMaybe<SortEnumType>;
  publishDate?: InputMaybe<SortEnumType>;
  relativeUrl?: InputMaybe<SortEnumType>;
  tenant?: InputMaybe<TenantSortInput>;
  tenantId?: InputMaybe<SortEnumType>;
  title?: InputMaybe<SortEnumType>;
  type?: InputMaybe<SortEnumType>;
};

export enum TaxonomyTypeEnum {
  Category = 'CATEGORY',
  Tag = 'TAG',
  Undefined = 'UNDEFINED',
}

export type TaxonomyTypeEnumOperationFilterInput = {
  eq?: InputMaybe<TaxonomyTypeEnum>;
  in?: InputMaybe<Array<TaxonomyTypeEnum>>;
  neq?: InputMaybe<TaxonomyTypeEnum>;
  nin?: InputMaybe<Array<TaxonomyTypeEnum>>;
};

export type Tenant = {
  __typename?: 'Tenant';
  allowedFileTypes: Array<Scalars['String']['output']>;
  id: Scalars['Int']['output'];
  name: Scalars['String']['output'];
  posts: Array<Post>;
  taxonomies: Array<Taxonomy>;
};

export type TenantFilterInput = {
  allowedFileTypes?: InputMaybe<ListStringOperationFilterInput>;
  and?: InputMaybe<Array<TenantFilterInput>>;
  id?: InputMaybe<IntOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<TenantFilterInput>>;
  posts?: InputMaybe<ListFilterInputTypeOfPostFilterInput>;
  taxonomies?: InputMaybe<ListFilterInputTypeOfTaxonomyFilterInput>;
};

export type TenantSortInput = {
  id?: InputMaybe<SortEnumType>;
  name?: InputMaybe<SortEnumType>;
};

export type Upload = {
  __typename?: 'Upload';
  author: ApplicationUser;
  authorId: Scalars['Int']['output'];
  children: Array<Upload>;
  cropName?: Maybe<Scalars['String']['output']>;
  description?: Maybe<Scalars['String']['output']>;
  fileName: Scalars['String']['output'];
  fullUrl?: Maybe<Scalars['String']['output']>;
  id: Scalars['Int']['output'];
  inTrash?: Maybe<Scalars['Boolean']['output']>;
  length: Scalars['Int']['output'];
  metaData?: Maybe<Scalars['String']['output']>;
  mimeType: Scalars['String']['output'];
  modifiedDate?: Maybe<Scalars['DateTime']['output']>;
  parent?: Maybe<Upload>;
  parentId?: Maybe<Scalars['Int']['output']>;
  publishDate: Scalars['DateTime']['output'];
  relativeUrl: Scalars['String']['output'];
  tenant: Tenant;
  tenantId: Scalars['Int']['output'];
  title?: Maybe<Scalars['String']['output']>;
};

export type UploadFilterInput = {
  and?: InputMaybe<Array<UploadFilterInput>>;
  author?: InputMaybe<ApplicationUserFilterInput>;
  authorId?: InputMaybe<IntOperationFilterInput>;
  children?: InputMaybe<ListFilterInputTypeOfUploadFilterInput>;
  cropName?: InputMaybe<StringOperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  fileName?: InputMaybe<StringOperationFilterInput>;
  fullUrl?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<IntOperationFilterInput>;
  inTrash?: InputMaybe<BooleanOperationFilterInput>;
  length?: InputMaybe<IntOperationFilterInput>;
  metaData?: InputMaybe<StringOperationFilterInput>;
  mimeType?: InputMaybe<StringOperationFilterInput>;
  modifiedDate?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<UploadFilterInput>>;
  parent?: InputMaybe<UploadFilterInput>;
  parentId?: InputMaybe<IntOperationFilterInput>;
  publishDate?: InputMaybe<DateTimeOperationFilterInput>;
  relativeUrl?: InputMaybe<StringOperationFilterInput>;
  tenant?: InputMaybe<TenantFilterInput>;
  tenantId?: InputMaybe<IntOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
};

export type UploadResponse = {
  __typename?: 'UploadResponse';
  description?: Maybe<Scalars['String']['output']>;
  fileName: Scalars['String']['output'];
  fullUrl?: Maybe<Scalars['String']['output']>;
  id: Scalars['Int']['output'];
  length: Scalars['Int']['output'];
  metaData?: Maybe<Scalars['String']['output']>;
  mimeType: Scalars['String']['output'];
  modifiedDate?: Maybe<Scalars['DateTime']['output']>;
  parentId?: Maybe<Scalars['Int']['output']>;
  publishDate: Scalars['DateTime']['output'];
  relativeUrl: Scalars['String']['output'];
  title?: Maybe<Scalars['String']['output']>;
};

export type UploadResponseFilterInput = {
  and?: InputMaybe<Array<UploadResponseFilterInput>>;
  description?: InputMaybe<StringOperationFilterInput>;
  fileName?: InputMaybe<StringOperationFilterInput>;
  fullUrl?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<IntOperationFilterInput>;
  length?: InputMaybe<IntOperationFilterInput>;
  metaData?: InputMaybe<StringOperationFilterInput>;
  mimeType?: InputMaybe<StringOperationFilterInput>;
  modifiedDate?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<UploadResponseFilterInput>>;
  parentId?: InputMaybe<IntOperationFilterInput>;
  publishDate?: InputMaybe<DateTimeOperationFilterInput>;
  relativeUrl?: InputMaybe<StringOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
};

export type UploadResponseSortInput = {
  description?: InputMaybe<SortEnumType>;
  fileName?: InputMaybe<SortEnumType>;
  fullUrl?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  length?: InputMaybe<SortEnumType>;
  metaData?: InputMaybe<SortEnumType>;
  mimeType?: InputMaybe<SortEnumType>;
  modifiedDate?: InputMaybe<SortEnumType>;
  parentId?: InputMaybe<SortEnumType>;
  publishDate?: InputMaybe<SortEnumType>;
  relativeUrl?: InputMaybe<SortEnumType>;
  title?: InputMaybe<SortEnumType>;
};

/** A connection to a list of items. */
export type UploadsConnection = {
  __typename?: 'UploadsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<UploadsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<UploadResponse>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** Identifies the total count of items in the connection. */
  totalCount: Scalars['Int']['output'];
};

/** An edge in a connection. */
export type UploadsEdge = {
  __typename?: 'UploadsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String']['output'];
  /** The item at the end of the edge. */
  node: UploadResponse;
};

export type UserResponse = {
  __typename?: 'UserResponse';
  accessFailedCount: Scalars['Int']['output'];
  email?: Maybe<Scalars['String']['output']>;
  emailConfirmed: Scalars['Boolean']['output'];
  id: Scalars['Int']['output'];
  lockoutEnabled: Scalars['Boolean']['output'];
  phoneNumber?: Maybe<Scalars['String']['output']>;
  twoFactorEnabled: Scalars['Boolean']['output'];
  userName?: Maybe<Scalars['String']['output']>;
};

export type UserResponseFilterInput = {
  accessFailedCount?: InputMaybe<IntOperationFilterInput>;
  and?: InputMaybe<Array<UserResponseFilterInput>>;
  email?: InputMaybe<StringOperationFilterInput>;
  emailConfirmed?: InputMaybe<BooleanOperationFilterInput>;
  id?: InputMaybe<IntOperationFilterInput>;
  lockoutEnabled?: InputMaybe<BooleanOperationFilterInput>;
  or?: InputMaybe<Array<UserResponseFilterInput>>;
  phoneNumber?: InputMaybe<StringOperationFilterInput>;
  twoFactorEnabled?: InputMaybe<BooleanOperationFilterInput>;
  userName?: InputMaybe<StringOperationFilterInput>;
};

export type UserResponseSortInput = {
  accessFailedCount?: InputMaybe<SortEnumType>;
  email?: InputMaybe<SortEnumType>;
  emailConfirmed?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  lockoutEnabled?: InputMaybe<SortEnumType>;
  phoneNumber?: InputMaybe<SortEnumType>;
  twoFactorEnabled?: InputMaybe<SortEnumType>;
  userName?: InputMaybe<SortEnumType>;
};

/** A connection to a list of items. */
export type UsersConnection = {
  __typename?: 'UsersConnection';
  /** A list of edges. */
  edges?: Maybe<Array<UsersEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<UserResponse>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** Identifies the total count of items in the connection. */
  totalCount: Scalars['Int']['output'];
};

/** An edge in a connection. */
export type UsersEdge = {
  __typename?: 'UsersEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String']['output'];
  /** The item at the end of the edge. */
  node: UserResponse;
};
