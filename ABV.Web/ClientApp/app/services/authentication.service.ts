import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'
import { isPlatformBrowser } from '@angular/common';

@Injectable()
export class AuthenticationService {

    public tokenUrl: string = "/api/token";
    public token: any;
    public loggedIn: boolean = false;
    public isAdmin: boolean = false;

    constructor( @Inject(PLATFORM_ID) private platformId: Object, private http: Http) {

    }

    login(username: string, password: string): Observable<boolean> {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');

            return this.http.post(
                this.tokenUrl,
                { Email: username, Password: password },
                { headers: headers })
                .map((response: Response) => {
                    let token = response.json() && response.json().token;
                    let isAdmin = response.json() && response.json().isAdmin;
                    if (isAdmin)
                        this.isAdmin = isAdmin;
                    if (token) {
                        if (isPlatformBrowser(this.platformId)) {
                            localStorage.setItem('auth_token', token);
                            localStorage.setItem('isAdmin', isAdmin);
                        }
                        this.loggedIn = true;
                        return this.loggedIn;
                    }
                    else {
                        return false;
                    }
                });
    }

    logout(): void {
        if (isPlatformBrowser(this.platformId)) {
            localStorage.removeItem('auth_token');
            localStorage.removeItem('isAdmin');
        }
        this.loggedIn = false;
        this.isAdmin = false;
    }

    getUserLoggedIn() {
        if (isPlatformBrowser(this.platformId)) {
            this.loggedIn = !!localStorage.getItem('auth_token');
            this.isAdmin = (localStorage.getItem('isAdmin') === 'true');
            return this.loggedIn;
        }
        else
            return false;
    }

    isAdminRole() {
        if (isPlatformBrowser(this.platformId))
            this.isAdmin = !!localStorage.getItem('isAdmin');
        return this.isAdmin;
    }

    getToken() {
        if (isPlatformBrowser(this.platformId))
            return localStorage.getItem('auth_token')
        else
            return null;
    }
}