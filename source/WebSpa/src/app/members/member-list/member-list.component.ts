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
   pagination: Pagination;

   constructor(
      private userService: UserService,
      private alertifyService: AlertifyService,
      private route: ActivatedRoute
   ) {}

   ngOnInit() {
      this.route.data.subscribe((data) => {
         this.users = data.users.result;

         this.pagination = JSON.parse(
            '{"currentPage":2,"itemsPerPage":3,"totalItems":7,"totalPages":3}'
         );

         console.log(this.pagination);

         // this.pagination = data.users.pagination;
      });
   }

   pageChanged(event: any): void {
      this.pagination.currentPage = event.page;
      console.log(this.pagination.currentPage);
      this.loadUsers();
   }

   loadUsers() {
      this.userService
         .getUsers(this.pagination.currentPage, this.pagination.itemsPerPage)
         .subscribe((paginatedUsers: PaginatedResult<User[]>) => {
            this.users = paginatedUsers.result;

            if (paginatedUsers.pagination === undefined) {
               this.pagination = JSON.parse(
                  '{"currentPage":' +
                     this.pagination.currentPage +
                     ',"itemsPerPage":' +
                     this.pagination.itemsPerPage +
                     ',"totalItems":7,"totalPages":3}'
               );
            } else {
               this.pagination = paginatedUsers.pagination;
            }
            console.log('paginatedUsers');
            console.log(paginatedUsers);
         });
   }
}
