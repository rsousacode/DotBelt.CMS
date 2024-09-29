/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface AccessTokenResponse {
  tokenType?: string | null;
  accessToken: string | null;
  /** @format int64 */
  expiresIn: number;
  refreshToken: string | null;
}

export interface ApplicationUser {
  /** @format int32 */
  id?: number;
  userName?: string | null;
  normalizedUserName?: string | null;
  email?: string | null;
  normalizedEmail?: string | null;
  emailConfirmed?: boolean;
  passwordHash?: string | null;
  securityStamp?: string | null;
  concurrencyStamp?: string | null;
  phoneNumber?: string | null;
  phoneNumberConfirmed?: boolean;
  twoFactorEnabled?: boolean;
  /** @format date-time */
  lockoutEnd?: string | null;
  lockoutEnabled?: boolean;
  /** @format int32 */
  accessFailedCount?: number;
}

export interface ForgotPasswordRequest {
  email: string | null;
}

export interface HttpValidationProblemDetails {
  type?: string | null;
  title?: string | null;
  /** @format int32 */
  status?: number | null;
  detail?: string | null;
  instance?: string | null;
  errors?: Record<string, string[]>;
  [key: string]: any;
}

export interface IFile {
  name?: string | null;
  /** @format int64 */
  length?: number | null;
  contentType?: string | null;
}

export interface InfoRequest {
  newEmail?: string | null;
  newPassword?: string | null;
  oldPassword?: string | null;
}

export interface InfoResponse {
  email: string | null;
  isEmailConfirmed: boolean;
}

export interface LoginRequest {
  email: string | null;
  password: string | null;
  twoFactorCode?: string | null;
  twoFactorRecoveryCode?: string | null;
}

export interface Post {
  /** @format int32 */
  id?: number;
  title?: string | null;
  description: string | null;
  relativeUrl: string | null;
  inTrash?: boolean | null;
  fullUrl?: string | null;
  content?: string | null;
  contentHtml?: string | null;
  /** @format date-time */
  publishDate?: string;
  /** @format date-time */
  modifiedDate?: string | null;
  author: ApplicationUser;
  /** @format int32 */
  authorId: number;
  postType?: PostTypeEnum;
  parentPost?: Post;
  /** @format int32 */
  parentPostId?: number | null;
  childrenPosts?: Post[] | null;
  taxonomies?: Taxonomy[] | null;
  tenant: Tenant;
  /** @format int32 */
  tenantId?: number;
  featuredImage?: Upload;
  /** @format int32 */
  featuredImageId?: number | null;
}

/** @format int32 */
export enum PostTypeEnum {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
}

export interface RefreshRequest {
  refreshToken: string | null;
}

export interface RegisterRequest {
  email: string | null;
  password: string | null;
}

export interface ResendConfirmationEmailRequest {
  email: string | null;
}

export interface ResetPasswordRequest {
  email: string | null;
  resetCode: string | null;
  newPassword: string | null;
}

export interface Taxonomy {
  /** @format int32 */
  id?: number;
  title?: string | null;
  relativeUrl: string | null;
  fullUrl?: string | null;
  inTrash?: boolean | null;
  /** @format date-time */
  publishDate?: string;
  /** @format date-time */
  modifiedDate?: string | null;
  tenant: Tenant;
  /** @format int32 */
  tenantId?: number;
  description: string | null;
  author: ApplicationUser;
  /** @format int32 */
  authorId: number;
  type?: TaxonomyTypeEnum;
  /** @format int32 */
  parentTaxonomyId?: number | null;
  posts?: Post[] | null;
}

/** @format int32 */
export enum TaxonomyTypeEnum {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
}

export interface Tenant {
  /** @format int32 */
  id?: number;
  name: string | null;
  allowedFileTypes: string[] | null;
  posts?: Post[] | null;
  taxonomies?: Taxonomy[] | null;
}

export interface TwoFactorRequest {
  enable?: boolean | null;
  twoFactorCode?: string | null;
  resetSharedKey?: boolean;
  resetRecoveryCodes?: boolean;
  forgetMachine?: boolean;
}

export interface TwoFactorResponse {
  sharedKey: string | null;
  /** @format int32 */
  recoveryCodesLeft: number;
  recoveryCodes?: string[] | null;
  isTwoFactorEnabled: boolean;
  isMachineRemembered: boolean;
}

export interface Upload {
  /** @format int32 */
  id?: number;
  title?: string | null;
  description?: string | null;
  fileName: string | null;
  absolutePath?: string | null;
  mimeType: string | null;
  /** @format int32 */
  length?: number;
  author: ApplicationUser;
  /** @format int32 */
  authorId?: number;
  relativeUrl: string | null;
  fullUrl?: string | null;
  inTrash?: boolean | null;
  /** @format date-time */
  publishDate?: string;
  /** @format date-time */
  modifiedDate?: string | null;
  tenant: Tenant;
  /** @format int32 */
  tenantId?: number;
  metaData?: string | null;
}

export namespace Api {
  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthRegisterCreate
   * @request POST:/api/auth/register
   */
  export namespace AuthRegisterCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = RegisterRequest;
    export type RequestHeaders = {};
    export type ResponseBody = void;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthLoginCreate
   * @request POST:/api/auth/login
   */
  export namespace AuthLoginCreate {
    export type RequestParams = {};
    export type RequestQuery = {
      useCookies?: boolean;
      useSessionCookies?: boolean;
    };
    export type RequestBody = LoginRequest;
    export type RequestHeaders = {};
    export type ResponseBody = AccessTokenResponse;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthLogoutCreate
   * @request POST:/api/auth/logout
   */
  export namespace AuthLogoutCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = void;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthRefreshCreate
   * @request POST:/api/auth/refresh
   */
  export namespace AuthRefreshCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = RefreshRequest;
    export type RequestHeaders = {};
    export type ResponseBody = AccessTokenResponse;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name MapIdentityApiApiAuthConfirmEmail
   * @request GET:/api/auth/confirmEmail
   */
  export namespace MapIdentityApiApiAuthConfirmEmail {
    export type RequestParams = {};
    export type RequestQuery = {
      userId: string;
      code: string;
      changedEmail?: string;
    };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = void;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthResendConfirmationEmailCreate
   * @request POST:/api/auth/resendConfirmationEmail
   */
  export namespace AuthResendConfirmationEmailCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = ResendConfirmationEmailRequest;
    export type RequestHeaders = {};
    export type ResponseBody = void;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthForgotPasswordCreate
   * @request POST:/api/auth/forgotPassword
   */
  export namespace AuthForgotPasswordCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = ForgotPasswordRequest;
    export type RequestHeaders = {};
    export type ResponseBody = void;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthResetPasswordCreate
   * @request POST:/api/auth/resetPassword
   */
  export namespace AuthResetPasswordCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = ResetPasswordRequest;
    export type RequestHeaders = {};
    export type ResponseBody = void;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthManage2FaCreate
   * @request POST:/api/auth/manage/2fa
   */
  export namespace AuthManage2FaCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = TwoFactorRequest;
    export type RequestHeaders = {};
    export type ResponseBody = TwoFactorResponse;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthManageInfoList
   * @request GET:/api/auth/manage/info
   */
  export namespace AuthManageInfoList {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = InfoResponse;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name AuthManageInfoCreate
   * @request POST:/api/auth/manage/info
   */
  export namespace AuthManageInfoCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = InfoRequest;
    export type RequestHeaders = {};
    export type ResponseBody = InfoResponse;
  }

  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name UploadsCreate
   * @request POST:/api/uploads
   */
  export namespace UploadsCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = IFile[];
    export type RequestHeaders = {};
    export type ResponseBody = Upload[];
  }
}

import type { AxiosInstance, AxiosRequestConfig, HeadersDefaults, ResponseType } from 'axios';
import axios from 'axios';

export type QueryParamsType = Record<string | number, any>;

export interface FullRequestParams
  extends Omit<AxiosRequestConfig, 'data' | 'params' | 'url' | 'responseType'> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseType;
  /** request body */
  body?: unknown;
}

export type RequestParams = Omit<FullRequestParams, 'body' | 'method' | 'query' | 'path'>;

export interface ApiConfig<SecurityDataType = unknown>
  extends Omit<AxiosRequestConfig, 'data' | 'cancelToken'> {
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<AxiosRequestConfig | void> | AxiosRequestConfig | void;
  secure?: boolean;
  format?: ResponseType;
}

export enum ContentType {
  Json = 'application/json',
  FormData = 'multipart/form-data',
  UrlEncoded = 'application/x-www-form-urlencoded',
  Text = 'text/plain',
}

export class HttpClient<SecurityDataType = unknown> {
  public instance: AxiosInstance;
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>['securityWorker'];
  private secure?: boolean;
  private format?: ResponseType;

  constructor({
    securityWorker,
    secure,
    format,
    ...axiosConfig
  }: ApiConfig<SecurityDataType> = {}) {
    this.instance = axios.create({
      ...axiosConfig,
      baseURL: axiosConfig.baseURL || '',
    });
    this.secure = secure;
    this.format = format;
    this.securityWorker = securityWorker;
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected mergeRequestParams(
    params1: AxiosRequestConfig,
    params2?: AxiosRequestConfig,
  ): AxiosRequestConfig {
    const method = params1.method || (params2 && params2.method);

    return {
      ...this.instance.defaults,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...((method &&
          this.instance.defaults.headers[method.toLowerCase() as keyof HeadersDefaults]) ||
          {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected stringifyFormItem(formItem: unknown) {
    if (typeof formItem === 'object' && formItem !== null) {
      return JSON.stringify(formItem);
    } else {
      return `${formItem}`;
    }
  }

  protected createFormData(input: Record<string, unknown>): FormData {
    if (input instanceof FormData) {
      return input;
    }
    return Object.keys(input || {}).reduce((formData, key) => {
      const property = input[key];
      const propertyContent: any[] = property instanceof Array ? property : [property];

      for (const formItem of propertyContent) {
        const isFileType = formItem instanceof Blob || formItem instanceof File;
        formData.append(key, isFileType ? formItem : this.stringifyFormItem(formItem));
      }

      return formData;
    }, new FormData());
  }

  public request = async <T = any, _E = any>({
    secure,
    path,
    type,
    query,
    format,
    body,
    ...params
  }: FullRequestParams): Promise<T> => {
    const secureParams =
      ((typeof secure === 'boolean' ? secure : this.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const responseFormat = format || this.format || undefined;

    if (type === ContentType.FormData && body && body !== null && typeof body === 'object') {
      body = this.createFormData(body as Record<string, unknown>);
    }

    if (type === ContentType.Text && body && body !== null && typeof body !== 'string') {
      body = JSON.stringify(body);
    }

    return this.instance
      .request({
        ...requestParams,
        headers: {
          ...(requestParams.headers || {}),
          ...(type ? { 'Content-Type': type } : {}),
        },
        params: query,
        responseType: responseFormat,
        data: body,
        url: path,
      })
      .then((response) => response.data);
  };
}

/**
 * @title DotBelt.CMS.API
 * @version 1.0
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthRegisterCreate
     * @request POST:/api/auth/register
     */
    authRegisterCreate: (data: RegisterRequest, params: RequestParams = {}) =>
      this.request<void, HttpValidationProblemDetails>({
        path: `/api/auth/register`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthLoginCreate
     * @request POST:/api/auth/login
     */
    authLoginCreate: (
      data: LoginRequest,
      query?: {
        useCookies?: boolean;
        useSessionCookies?: boolean;
      },
      params: RequestParams = {},
    ) =>
      this.request<AccessTokenResponse, any>({
        path: `/api/auth/login`,
        method: 'POST',
        query: query,
        body: data,
        type: ContentType.Json,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthLogoutCreate
     * @request POST:/api/auth/logout
     */
    authLogoutCreate: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/auth/logout`,
        method: 'POST',
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthRefreshCreate
     * @request POST:/api/auth/refresh
     */
    authRefreshCreate: (data: RefreshRequest, params: RequestParams = {}) =>
      this.request<AccessTokenResponse, any>({
        path: `/api/auth/refresh`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name MapIdentityApiApiAuthConfirmEmail
     * @request GET:/api/auth/confirmEmail
     */
    mapIdentityApiApiAuthConfirmEmail: (
      query: {
        userId: string;
        code: string;
        changedEmail?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/auth/confirmEmail`,
        method: 'GET',
        query: query,
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthResendConfirmationEmailCreate
     * @request POST:/api/auth/resendConfirmationEmail
     */
    authResendConfirmationEmailCreate: (
      data: ResendConfirmationEmailRequest,
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/auth/resendConfirmationEmail`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthForgotPasswordCreate
     * @request POST:/api/auth/forgotPassword
     */
    authForgotPasswordCreate: (data: ForgotPasswordRequest, params: RequestParams = {}) =>
      this.request<void, HttpValidationProblemDetails>({
        path: `/api/auth/forgotPassword`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthResetPasswordCreate
     * @request POST:/api/auth/resetPassword
     */
    authResetPasswordCreate: (data: ResetPasswordRequest, params: RequestParams = {}) =>
      this.request<void, HttpValidationProblemDetails>({
        path: `/api/auth/resetPassword`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthManage2FaCreate
     * @request POST:/api/auth/manage/2fa
     */
    authManage2FaCreate: (data: TwoFactorRequest, params: RequestParams = {}) =>
      this.request<TwoFactorResponse, HttpValidationProblemDetails | void>({
        path: `/api/auth/manage/2fa`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthManageInfoList
     * @request GET:/api/auth/manage/info
     */
    authManageInfoList: (params: RequestParams = {}) =>
      this.request<InfoResponse, HttpValidationProblemDetails | void>({
        path: `/api/auth/manage/info`,
        method: 'GET',
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name AuthManageInfoCreate
     * @request POST:/api/auth/manage/info
     */
    authManageInfoCreate: (data: InfoRequest, params: RequestParams = {}) =>
      this.request<InfoResponse, HttpValidationProblemDetails | void>({
        path: `/api/auth/manage/info`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags DotBelt.CMS.API
     * @name UploadsCreate
     * @request POST:/api/uploads
     */
    uploadsCreate: (data: IFile[], params: RequestParams = {}) =>
      this.request<Upload[], HttpValidationProblemDetails>({
        path: `/api/uploads`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        format: 'json',
        ...params,
      }),
  };
}
