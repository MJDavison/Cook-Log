import { Injectable } from '@angular/core';

export type IAuthenticationResult =
  SuccessAuthenticationResult |
  FailureAuthenticationResult |
  RedirectAuthenticationResult;


  export interface SuccessAuthenticationResult {
    status: AuthenticationResultStatus.Success;
    state: any;
  }

  export interface FailureAuthenticationResult {
    status: AuthenticationResultStatus.Fail;
    message: string;
  }

  export interface RedirectAuthenticationResult {
    status: AuthenticationResultStatus.Redirect;
  }

  export enum AuthenticationResultStatus {
    Success,
    Redirect,
    Fail
  }

  @Injectable({
    providedIn: 'root'
  })

export class AuthorizeService{


private error(message: string): IAuthenticationResult{
  return {status: AuthenticationResultStatus.Fail, message};
}
}
