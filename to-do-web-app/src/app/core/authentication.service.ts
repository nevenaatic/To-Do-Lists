import { Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  isLoggedIn = false;
  constructor(private readonly msalService: MsalService) { }

  readonly isAuthenticated = () => {
    if(this.msalService.instance.getAllAccounts().length > 0){
      return this.isLoggedIn = true;
    }
     return  this.isLoggedIn;
  }
}
