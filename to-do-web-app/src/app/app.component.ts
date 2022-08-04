import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MsalBroadcastService, MsalGuardConfiguration, MsalService, MSAL_GUARD_CONFIG } from '@azure/msal-angular';
import { InteractionStatus, PopupRequest } from '@azure/msal-browser';
import { Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';
import { loginRequest } from './auth-config';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    loginDisplay = true;
    activeUser = '';

    constructor(private readonly router: Router,
        private authService: MsalService) {
        this.setLoginDisplay();
    }

    readonly login = () => {
        this.authService.loginPopup(loginRequest)
            .subscribe(_ => this.setLoginDisplay());
    }

    readonly logOut = () => {
        this.authService.logout();
    }

    setLoginDisplay() {
        this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
        if (this.loginDisplay) {
            let activeAccount = this.authService.instance.getAllAccounts()[0];
            this.authService.instance.setActiveAccount(activeAccount);
            this.activeUser = activeAccount.idTokenClaims?.name as string ?? '';
            this.router.navigate(['dashboard']);
        }else{            
            this.authService.instance.setActiveAccount(null);
            sessionStorage.removeItem('msal.interaction.status');
        }
    }
}
