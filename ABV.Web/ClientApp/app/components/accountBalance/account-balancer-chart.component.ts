import { Component, Inject } from '@angular/core';
import { AccountBalanceService } from "../../services/account-balance.service";
import { AccountBalanceChart } from "../../models/account-balance-chart";

@Component({
    selector: 'line-chart',
    templateUrl: './account-balancer-chart.component.html'
})
export class AccountBalancerChartComponent {

    chartData: AccountBalanceChart[] = [];
    chartLabels: string[];

    constructor(private accountBalanceService: AccountBalanceService) {
       
    }

    ngOnInit() {
        this.getChartData();
        this.getChartLabels();
    }

    getChartLabels() {
        this.accountBalanceService.getChartLabels().subscribe((labels) => {
            this.chartLabels = labels;
        },
        (error) => {
                
        });
    }

    getChartData() {
        this.accountBalanceService.getChartData().subscribe((data) => {
            this.chartData = data;
        },
        (error) => {
                
        });
    }

    chartOptions = {
        responsive: true
      };

    onChartClick(event: any) {
        console.log(event);
    }
}
