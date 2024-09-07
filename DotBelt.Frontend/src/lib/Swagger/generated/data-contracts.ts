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
