import { ClubsService } from '@proxy/club-management/clubs';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { PagedResultDto } from '@abp/ng.core';
import { ClubDto } from '@proxy/club-management/clubs/dto';

@Component({
  selector: 'app-clubs',
  templateUrl: './clubs.component.html',
  styleUrls: ['./clubs.component.scss'],
})
export class ClubsComponent implements OnInit, OnDestroy {
  destroy$ = new Subject();
  getClubsListResult: PagedResultDto<ClubDto>;

  constructor(private clubsService: ClubsService) {}

  ngOnInit(): void {
    this.getClubsList();
  }

  getClubsList(): void {
    this.clubsService
      .getList({ maxResultCount: 20 })
      .pipe(takeUntil(this.destroy$))
      .subscribe(result => {
        this.getClubsListResult = result;
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
