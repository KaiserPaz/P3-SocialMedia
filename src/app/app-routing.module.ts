import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { HomePageComponent } from './home-page/home-page.component';
import { AboutPageComponent } from './about-page/about-page.component';
import { SeenCommentsPageComponent } from './seen-comments-page/seen-comments-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { SignUpPageComponent } from './sign-up-page/sign-up-page.component';
import { UserProfilePageComponent } from './user-profile-page/user-profile-page.component';

import { DashboardPageComponent } from './dashboard-page/dashboard-page.component';


const routes: Routes = [
  {path: '', component: HomePageComponent},
  {path: 'homePage', component: HomePageComponent},
  {path: 'aboutPage', component: AboutPageComponent},
  {path: 'seenCommentsPage', component: SeenCommentsPageComponent},
  {path: 'loginPage', component: LoginPageComponent},
  {path: 'signUpPage', component: SignUpPageComponent},
  {path: 'userProfilePage', component: UserProfilePageComponent},
  {path: 'dashboardPage', component: DashboardPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
