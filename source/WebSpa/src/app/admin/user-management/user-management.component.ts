import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { ValueTransformer } from '@angular/compiler/src/util';

@Component({
   selector: 'app-user-management',
   templateUrl: './user-management.component.html',
   styleUrls: ['./user-management.component.css'],
})
export class UserManagementComponent implements OnInit {
   users: User[];
   bsModalRef: BsModalRef;

   constructor(
      private modalService: BsModalService,
      private adminService: AdminService
   ) {}

   ngOnInit() {
      this.getUserWithRoles();
   }

   getUserWithRoles() {
      this.adminService.getUsersWithRoles().subscribe(
         (users: User[]) => {
            this.users = users;
         },
         (error) => {
            console.log(error);
         }
      );
   }

   editRolesModal(user: User) {
      const initialState = {
         user,
         roles: this.getRoles(user),
      };
      this.bsModalRef = this.modalService.show(RolesModalComponent, {
         initialState,
      });
      this.bsModalRef.content.updateSelectedRoles.subscribe((value) => {
         const rolesToUpdate = {
            roleNames: [
               ...value
                  .filter((el) => el.checked === true)
                  .map((el) => el.name),
            ],
         };


         if (rolesToUpdate) {
            this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(
               () => {
                  user.roles = [...rolesToUpdate.roleNames];
               },
               (error) => {
                  console.log(error);
               }
            );
         }
      });
   }

   private getRoles(user) {
      const roles = [];
      const userRoles = user.roles;
      const availableRoles: any[] = [
         { name: 'Admin', value: 'Admin' },
         { name: 'Moderator', value: 'Moderator' },
         { name: 'Member', value: 'Member' },
         { name: 'VIP', value: 'VIP' },
      ];

      // tslint:disable-next-line: prefer-for-of
      for (let i = 0; i < availableRoles.length; i++) {
         let isMatch = false;
         // tslint:disable-next-line: prefer-for-of
         for (let j = 0; j < userRoles.length; j++) {
            if (availableRoles[i].name === userRoles[j]) {
               isMatch = true;
               availableRoles[i].checked = true;
               roles.push(availableRoles[i]);
               break;
            }
         }
         if (!isMatch) {
            availableRoles[i].checked = false;
            roles.push(availableRoles[i]);
         }
      }
      return roles;
   }
}
