import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { AdminGuard } from './admin.guard';
import { HomeComponent } from './home/home.component';
import { AdminComponent } from './admin.component';
import { UserComponent } from './user/user.component';
import { ResourceEditComponent } from './resource/resource-edit.component';
import { FolderEditComponent } from './folder/folder-edit.component';
import { RulesComponent } from './rules/rules.component';
import { AdminBookingComponent } from './bookings/bookings.admin.component';
import { UserReadComponent } from './user/user-read.component';
import { UserCreateComponent } from './user/user-create.cpmponent';
import { UserListComponent } from './user/user-list.component';
import { UserChangeComponent } from './user/user-edit.component';


const routesAdmin: Routes = [
  {
    path: '', component: AdminComponent, canActivate: [AdminGuard], children: [
      { path: '', component: HomeComponent, data: { breadcrumbIgnore: true } },
      {
        path: 'users', component: UserComponent, data: { breadcrumbLabel: 'Users Management' }, children: [
          { path: '', component: UserListComponent, data: { breadcrumbLabel: 'Users List', breadcrumbIgnore: true } },
          { path: 'create', component: UserCreateComponent, data: { breadcrumbLabel: 'Create Admin' } },
          { path: ':id', component: UserReadComponent, data: { breadcrumbLabel: 'User Details' } },
          { path: ':id/edit', component: UserChangeComponent, data: { breadcrumbLabel: 'User Details' } },
        ]
      },
      { path: 'resources/create', component: ResourceEditComponent, data: { breadcrumbLabel: 'Resource Creation' } },
      { path: 'resources/:id/edit', component: ResourceEditComponent, data: { breadcrumbLabel: 'Resource Editing' } },
      { path: 'folders/create', component: FolderEditComponent, data: { breadcrumbLabel: 'Folder Creation' } },
      { path: 'folders/:id/edit', component: FolderEditComponent, data: { breadcrumbLabel: 'Folder Editing' } },
      { path: 'rules', component: RulesComponent, data: { breadcrumbLabel: 'Rules management' } },
      { path: 'rules/:id', component: RulesComponent, data: {breadcrumbLabel: 'Rules management'} },
      { path: 'rules/:id/edit', component: RulesComponent, data: {breadcrumbLabel: 'Rules management'} },
      { path: 'bookings', component: AdminBookingComponent, data: { breadcrumbLabel: 'All bookings managment' } }
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routesAdmin)
  ],
  exports: [RouterModule],
  declarations: []
})
export class AdminRoutingModule { }
