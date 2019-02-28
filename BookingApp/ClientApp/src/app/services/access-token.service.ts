import * as jwt_decode from "jwt-decode";
import { Logger } from './logger.service';
import { Injectable, EventEmitter, Output } from "@angular/core";

@Injectable()
export class AccessTokenService {

  private accTokenName = 'accessToken';
  private accTokenExpName = 'accessTokenExpires';
  private roleKey = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

  @Output() TokenExpired: EventEmitter<any> = new EventEmitter();

  public writeToken(accessToken: string): void {
    localStorage.setItem(this.accTokenName, accessToken);
    localStorage.setItem(this.accTokenExpName, (Date.now() + 1000 * 60 * 15).toString() ); // 15-minute STUB
  }

  public readToken(): string {
    let token = localStorage.getItem(this.accTokenName);

    if (token != undefined && Date.now() > +localStorage.getItem(this.accTokenExpName)) {
        Logger.warn("JWT expired.");
        this.deleteToken();
        this.TokenExpired.emit();
        token = undefined;
    }

    return token;
  }

  public deleteToken(): void {
    localStorage.removeItem(this.accTokenName);
    localStorage.removeItem(this.accTokenExpName);
  }
  
  private readTokenDecoded() {
    let rawToken = this.readToken();

    if (rawToken == undefined)
      return undefined;
    else
      return jwt_decode(this.readToken());
  }

  readUsername() {
    let decodedToken = this.readTokenDecoded();
    if (decodedToken == undefined)
      return undefined;
    else
      return this.readTokenDecoded().sub;
  }

  readRoles() {
    let decodedToken = this.readTokenDecoded();
    if (decodedToken == undefined)
      return undefined;
    else {
      let roles = decodedToken[this.roleKey];

      if (!(roles instanceof Array))
        roles = [roles];

      return roles;
    }
  }
}