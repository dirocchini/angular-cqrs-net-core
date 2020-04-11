import { Component, OnInit } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { User } from '../../_models/user';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from '../../_models/pagination';
import { PaginationComponent } from 'ngx-bootstrap';

@Component({
   selector: 'app-member-list',
   templateUrl: './member-list.component.html',
   styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
   users: User[];
   user: User = JSON.parse(localStorage.getItem('user'));
   genderList = [
      { value: 'male', display: 'Males' },
      { value: 'female', display: 'Females' },
   ];
   userParams: any = {};
   pagination: Pagination;

   constructor(
      private userService: UserService,
      private alertifyService: AlertifyService,
      private route: ActivatedRoute
   ) {}

   ngOnInit() {
      this.route.data.subscribe((data) => {
         this.users = data.users.result;
         this.pagination = data.users.pagination;
      });

      this.userParams.gender = this.user.gender === 'female' ? 'male' : 'female';
      this.userParams.minAge = 18;
      this.userParams.maxAge = 90;
      this.userParams.orderBy = 'lastActive';
   }

   pageChanged(event: any): void {
      this.pagination.currentPage = event.page;
      this.loadUsers();
   }

   resetFilters() {
      this.userParams.gender = this.user.gender === 'female' ? 'male' : 'female';
      this.userParams.minAge = 18;
      this.userParams.maxAge = 90;
      this.userParams.orderBy = 'lastActive';

      this.loadUsers();
   }

   loadUsers() {
      this.userService
         .getUsers(
            this.pagination.currentPage,
            this.pagination.itemsPerPage,
            this.userParams
         )
         .subscribe((paginatedUsers: PaginatedResult<User[]>) => {
            this.users = paginatedUsers.result;
            this.pagination = paginatedUsers.pagination;
         });
   }
}
