import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from "../../services/authentication.service";

@Component({
    selector: 'login-manager',
    templateUrl: './login.component.html'
})
export class LoginComponent {
    username: string;
    password: string;
    errorMessage: string = '';
    constructor(private router: Router, private auhenticationService: AuthenticationService) { }

    ngOnInit() { }

    loginUser() {
        this.auhenticationService.login(this.username, this.password)
            .subscribe(res => {
                if (res === true) {
                    this.router.navigate(['home']);
                }
            },
            (error) => {
                this.errorMessage = error._body;
                if (error.status === 401)
                    this.errorMessage = 'Invalid Username or Password. Please try again with the correct credentials.'
            });
    }
}