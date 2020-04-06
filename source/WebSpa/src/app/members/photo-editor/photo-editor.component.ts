import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../../environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
   selector: 'app-photo-editor',
   templateUrl: './photo-editor.component.html',
   styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
   @Input() photos: Photo[];
   @Input() uploader: FileUploader;
   @Output() getMemberPhotoChange = new EventEmitter<string>();

   hasBaseDropZoneOver: boolean;
   response: string;
   baseUrl = environment.apiUrl;
   currentMain: Photo;

   constructor(
      private authService: AuthService,
      private userService: UserService,
      private alertify: AlertifyService
   ) {
      this.uploader = new FileUploader({
         url:
            this.baseUrl +
            'user/' +
            this.authService.decodedToken.nameid +
            '/photos',
         authToken: 'Bearer ' + localStorage.getItem('token'),
         isHTML5: true,
         allowedFileType: ['image'],
         removeAfterUpload: true,
         autoUpload: false,
         maxFileSize: 10 * 1024 * 1024
      });
      this.uploader.onAfterAddingFile = file => {
         file.withCredentials = false;
      };
      this.hasBaseDropZoneOver = false;

      this.response = '';

      this.uploader.response.subscribe(res => (this.response = res));

      this.uploader.onSuccessItem = (item, response, status, headers) => {
         if (response) {
            const res: Photo = JSON.parse(response);
            const photo = {
               id: res.id,
               url: res.url,
               description: res.description,
               isMain: res.isMain
            };
            this.photos.push(photo);
         }
      };
   }

   public fileOverBase(e: any): void {
      this.hasBaseDropZoneOver = e;
   }

   public setMainPhoto(photo: Photo) {
      this.userService
         .setMainPhoto(this.authService.decodedToken.nameid, photo.id)
         .subscribe(
            () => {
               this.currentMain = this.photos.filter(p => p.isMain === true)[0];
               this.currentMain.isMain = false;
               photo.isMain = true;
               this.authService.changeMemberPhoto(photo.url);
               this.authService.currentUser.photoUrl = photo.url;
               localStorage.setItem(
                  'user',
                  JSON.stringify(this.authService.currentUser)
               );
            },
            error => {
               this.alertify.error(error);
            }
         );
   }

   public deletePhoto(id: number) {
      this.alertify.confirm('Really?', () => {
         this.userService.deletePhoto(this.authService.decodedToken.nameid, id).subscribe(
               () => {
                  this.photos.splice(
                     this.photos.findIndex(p => (p.id = id)),
                     1
                  );
                  this.alertify.success('Photo deleted');
               },
               error => {
                  this.alertify.error(error);
               }
            );
      });
   }

   ngOnInit() {}
}
