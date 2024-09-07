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

import {
  AccessTokenResponse,
  ForgotPasswordRequest,
  HttpValidationProblemDetails,
  InfoRequest,
  InfoResponse,
  LoginRequest,
  RefreshRequest,
  RegisterRequest,
  ResendConfirmationEmailRequest,
  ResetPasswordRequest,
  TwoFactorRequest,
  TwoFactorResponse,
} from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class DotBeltCmsApi<SecurityDataType = unknown> {
  http: HttpClient<SecurityDataType>;

  constructor(http: HttpClient<SecurityDataType>) {
    this.http = http;
  }

  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name GetRoot
   * @request GET:/
   */
  getRoot = (params: RequestParams = {}) =>
    this.http.request<string, any>({
      path: `/`,
      method: "GET",
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name AuthRegisterCreate
   * @request POST:/api/auth/register
   */
  authRegisterCreate = (data: RegisterRequest, params: RequestParams = {}) =>
    this.http.request<void, HttpValidationProblemDetails>({
      path: `/api/auth/register`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name AuthLoginCreate
   * @request POST:/api/auth/login
   */
  authLoginCreate = (
    data: LoginRequest,
    query?: {
      useCookies?: boolean;
      useSessionCookies?: boolean;
    },
    params: RequestParams = {},
  ) =>
    this.http.request<AccessTokenResponse, any>({
      path: `/api/auth/login`,
      method: "POST",
      query: query,
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name AuthRefreshCreate
   * @request POST:/api/auth/refresh
   */
  authRefreshCreate = (data: RefreshRequest, params: RequestParams = {}) =>
    this.http.request<AccessTokenResponse, any>({
      path: `/api/auth/refresh`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name MapIdentityApiApiAuthConfirmEmail
   * @request GET:/api/auth/confirmEmail
   */
  mapIdentityApiApiAuthConfirmEmail = (
    query?: {
      userId?: string;
      code?: string;
      changedEmail?: string;
    },
    params: RequestParams = {},
  ) =>
    this.http.request<void, any>({
      path: `/api/auth/confirmEmail`,
      method: "GET",
      query: query,
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name AuthResendConfirmationEmailCreate
   * @request POST:/api/auth/resendConfirmationEmail
   */
  authResendConfirmationEmailCreate = (data: ResendConfirmationEmailRequest, params: RequestParams = {}) =>
    this.http.request<void, any>({
      path: `/api/auth/resendConfirmationEmail`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name AuthForgotPasswordCreate
   * @request POST:/api/auth/forgotPassword
   */
  authForgotPasswordCreate = (data: ForgotPasswordRequest, params: RequestParams = {}) =>
    this.http.request<void, HttpValidationProblemDetails>({
      path: `/api/auth/forgotPassword`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name AuthResetPasswordCreate
   * @request POST:/api/auth/resetPassword
   */
  authResetPasswordCreate = (data: ResetPasswordRequest, params: RequestParams = {}) =>
    this.http.request<void, HttpValidationProblemDetails>({
      path: `/api/auth/resetPassword`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name AuthManage2FaCreate
   * @request POST:/api/auth/manage/2fa
   */
  authManage2FaCreate = (data: TwoFactorRequest, params: RequestParams = {}) =>
    this.http.request<TwoFactorResponse, HttpValidationProblemDetails | void>({
      path: `/api/auth/manage/2fa`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name AuthManageInfoList
   * @request GET:/api/auth/manage/info
   */
  authManageInfoList = (params: RequestParams = {}) =>
    this.http.request<InfoResponse, HttpValidationProblemDetails | void>({
      path: `/api/auth/manage/info`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags DotBelt.CMS.API
   * @name AuthManageInfoCreate
   * @request POST:/api/auth/manage/info
   */
  authManageInfoCreate = (data: InfoRequest, params: RequestParams = {}) =>
    this.http.request<InfoResponse, HttpValidationProblemDetails | void>({
      path: `/api/auth/manage/info`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
}
