import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClubManagementComponent } from './club-management.component';
import { ClubsComponent } from './clubs/clubs.component';

const routes: Routes = [
  {
    path: '',
    component: ClubManagementComponent,
    children: [
      { path: 'clubs', component: ClubsComponent },
      { path: '**', pathMatch: 'full', redirectTo: 'clubs' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClubManagementRoutingModule {}
