import { Component, OnInit, Input } from '@angular/core';
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
   hasBaseDropZoneOver: boolean;
   response: string;
   baseUrl = environment.apiUrl;

   constructor(
      private authService: AuthService,
      private userService: UserService,
      private aletify: AlertifyService
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
         // disableMultipart: true, // 'DisableMultipart' must be 'true' for formatDataFunction to be called.
         // formatDataFunctionIsAsync: true,
         // formatDataFunction: async item => {
         //    return new Promise((resolve, reject) => {
         //       resolve({
         //          name: item._file.name,
         //          length: item._file.size,
         //          contentType: item._file.type,
         //          date: new Date()
         //       });
         //    });
         // }
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
               console.log('photo is now main');
            },
            error => {
               this.aletify.error(error);
            }
         );
   }

   ngOnInit() {}
}
