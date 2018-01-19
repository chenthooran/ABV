import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions  } from '@angular/http';
import { AccountBalancesListItemModel } from "../models/account-balances-item";
import { AccountBalanceChart } from "../models/account-balance-chart";
import { AuthenticationService } from "./authentication.service";
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/do'
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';


@Injectable()
export class AccountBalanceService {
    constructor(private _http: Http, private auhenticationService: AuthenticationService) {

    }

    private readonly accountBalanceUrl: string = '/api/AccountBalance/Latest';
    private readonly chartDataUrl: string = '/api/AccountBalance/ChartData';
    private readonly chartLabelsUrl: string = '/api/AccountBalance/ChartLabels';

    public getLatestAccountBalances(): Observable<AccountBalancesListItemModel[]> {
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.auhenticationService.getToken() });
        let options = new RequestOptions({ headers: headers });

        return this._http.get(this.accountBalanceUrl, options)
            .map((response) => {
                return response.json() as AccountBalancesListItemModel[];
            })
            .do((comp) => {
                
            }) //Optional
            .catch((error: string) => {
                return Observable.throw(`There is an error while loading data: ${error}`);
            });
    }

    public getChartData(): Observable<AccountBalanceChart[]> {
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.auhenticationService.getToken() });
        let options = new RequestOptions({ headers: headers });

        return this._http.get(this.chartDataUrl, options)
            .map((response) => {
                return response.json() as AccountBalanceChart[];
            })
            .do((comp) => {
                
            }) //Optional
            .catch((error: string) => {
                return Observable.throw(`There is an error while loading data: ${error}`);
            });
    }

    public getChartLabels(): Observable<string[]> {
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.auhenticationService.getToken() });
        let options = new RequestOptions({ headers: headers });

        return this._http.get(this.chartLabelsUrl, options)
            .map((response) => {
                return response.json() as string[];
            })
            .do((comp) => {
                
            }) //Optional
            .catch((error: string) => {
                return Observable.throw(`There is an error while loading data: ${error}`);
            });
    }
}