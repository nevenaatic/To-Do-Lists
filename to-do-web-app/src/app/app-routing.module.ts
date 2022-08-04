import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ToDoListEditComponent } from './components/to-do-list-edit/to-do-list-edit.component';
import { ToDoListShareComponent } from './components/to-do-list-share/to-do-list-share.component';
import { AuthGuard } from './core/auth.guard';
const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'to-do-list/:id', component: ToDoListEditComponent, canActivate: [AuthGuard] },
  { path: 'to-do-list', component: ToDoListEditComponent, canActivate: [AuthGuard] },
  { path: 'shared-list/:id', component: ToDoListShareComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
