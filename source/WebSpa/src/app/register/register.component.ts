import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
   selector: 'app-register',
   templateUrl: './register.component.html',
   styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
   @Output() cancelRegister = new EventEmitter();

   model: any = {};

   registerForm: FormGroup;

   constructor(
      private authService: AuthService,
      private alertify: AlertifyService
   ) {}

   ngOnInit() {
      this.registerForm = new FormGroup({
         name: new FormControl(),
         login: new FormControl(),
         password: new FormControl(),
         confirmPassword: new FormControl()
      });
   }

   register() {
      console.log(this.registerForm.value);
      // //  console.log(this.model);
      // this.authService.register(this.model).subscribe(
      //    () => {
      //       this.alertify.success('Registered successfully');
      //    },
      //    error => {
      //       this.alertify.error(error);
      //    }
      // );
   }

   cancel() {
      this.cancelRegister.emit(false);
      this.alertify.message('Registration cancelled');
   }
}
