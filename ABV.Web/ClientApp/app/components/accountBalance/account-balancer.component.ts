import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { AccountBalanceService } from "../../services/account-balance.service";
import { AccountBalancesListItemModel } from "../../models/account-balances-item";

@Component({
    selector: 'fetch-latest-balances',
    templateUrl: './account-balancer.component.html'
})
export class AccountBalancerComponent {

    public accountBalances: AccountBalancesListItemModel[];
    public asOfDate: Date;

    constructor(private accountBalanceService: AccountBalanceService) {
       
    }

    ngOnInit() {
        this.getaccountBalance();
    }

    getaccountBalance() {
        this.accountBalanceService.getLatestAccountBalances().subscribe((data) => {
            this.accountBalances = data;
            this.asOfDate = this.accountBalances[0].asOfDate;
        },
        (error) => {
                
        });
    }
}
