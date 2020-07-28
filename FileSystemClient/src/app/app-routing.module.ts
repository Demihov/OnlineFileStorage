import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from '../app/home/home.component';
import { LoginComponent } from '../app/login/login.component';
import { AuthGuard } from './_guards/auth.guard';
import { AboutComponent } from './about/about.component';
import { TableComponent } from './table/table.component';

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    {path: 'about', component: AboutComponent},
    {path: 'table', component: TableComponent},

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);