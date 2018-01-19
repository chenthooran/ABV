import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from "../../services/authentication.service";

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    
    constructor(private router: Router, private _auhenticationService: AuthenticationService) {
        this.auhenticationService = _auhenticationService;
    }

    public auhenticationService: AuthenticationService;
    public isLoggedIn: boolean = false;

    ngOnInit() {
        this.isLoggedIn = this.auhenticationService.loggedIn
    }

    logoutUser() {
        this.isLoggedIn = this.auhenticationService.loggedIn;
        this.auhenticationService.logout();
        this.router.navigate(['login']);
    }
}
