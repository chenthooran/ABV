import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ChartsModule } from 'ng2-charts';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { FileUploaderComponent } from './components/fileUploader/file-uploader.component';
import { AccountBalancerComponent } from "./components/accountBalance/account-balancer.component";
import { AccountBalancerChartComponent } from "./components/accountBalance/account-balancer-chart.component";
import { AccountBalanceService } from "./services/account-balance.service";
import { AuthenticationService } from "./services/authentication.service";
import { AuthguardService } from "./services/authguard.service";
import { LoginComponent } from "./components/login/login.component";


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        FileUploaderComponent,
        AccountBalancerComponent,
        AccountBalancerChartComponent,
        LoginComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ChartsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'login', pathMatch: 'full' },
            { path: 'login', component: LoginComponent },
            { path: 'home', component: AccountBalancerComponent, canActivate: [AuthguardService] },
            { path: 'line-chart', component: AccountBalancerChartComponent, canActivate: [AuthguardService] },
            { path: 'file-upload', component: FileUploaderComponent, canActivate: [AuthguardService] },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        AccountBalanceService,
        AuthenticationService,
        AuthguardService,
    ]
})
export class AppModuleShared {
}
