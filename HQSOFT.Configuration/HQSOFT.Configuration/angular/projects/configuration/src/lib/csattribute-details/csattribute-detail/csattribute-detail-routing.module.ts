import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CSAttributeDetailComponent } from './components/csattribute-detail.component';

const routes: Routes = [
  {
    path: '',
    component: CSAttributeDetailComponent,
    canActivate: [AuthGuard, PermissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CSAttributeDetailRoutingModule {}
