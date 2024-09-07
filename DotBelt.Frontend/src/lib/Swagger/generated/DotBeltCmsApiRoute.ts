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

export namespace DotBeltCmsApi {
  /**
   * No description
   * @tags DotBelt.CMS.API
   * @name GetRoot
   * @request GET:/
   */
  export namespace GetRoot {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = string;
  }

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
      userId?: string;
      code?: string;
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
}
