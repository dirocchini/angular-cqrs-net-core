import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { User } from '../_models/user';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
   selector: 'app-lists',
   templateUrl: './lists.component.html',
   styleUrls: ['./lists.component.css'],
})
export class ListsComponent implements OnInit {
   users: User[];
   pagination: Pagination;
   likesParam: string;

   constructor(
      private authService: AuthService,
      private userService: UserService,
      private route: ActivatedRoute,
      private alertify: AlertifyService
   ) {}

   ngOnInit() {
      this.route.data.subscribe((data) => {
         this.users = data.users.result;
         // this.pagination = data.users.pagination;

         this.pagination = JSON.parse(
            '{"currentPage":2,"itemsPerPage":3,"totalItems":7,"totalPages":3}'
         );
      });

      this.likesParam = 'Likers';
   }

   loadUsers() {

      this.userService
         .getUsers(
            this.pagination.currentPage,
            this.pagination.itemsPerPage,
            null,
            this.likesParam
         )
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

   pageChanged(event: any): void {
      this.pagination.currentPage = event.page;
      console.log(this.pagination.currentPage);
      this.loadUsers();
   }
}
