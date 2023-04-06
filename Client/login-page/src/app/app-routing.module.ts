import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home/home.component';
import { UserComponent } from './pages/user/user.component';
import { AuthGuardService } from './shared/services/authen.service';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { 
    path: 'home',
    component: HomeComponent,
   },
  { 
    path: 'user', 
    component: UserComponent,
    canActivate: [ AuthGuardService ] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  providers: [AuthGuardService],
  exports: [RouterModule]
})
export class AppRoutingModule { }
