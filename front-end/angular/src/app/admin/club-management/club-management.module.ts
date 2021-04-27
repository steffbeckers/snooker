import { ClubManagementComponent } from './club-management.component';
import { ClubManagementRoutingModule } from './club-management-routing.module';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { ClubsComponent } from './clubs/clubs.component';

@NgModule({
  declarations: [ClubManagementComponent, ClubsComponent],
  imports: [SharedModule, ClubManagementRoutingModule],
})
export class ClubManagementModule {}
