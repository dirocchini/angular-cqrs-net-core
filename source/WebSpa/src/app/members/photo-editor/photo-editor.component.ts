import { Component, OnInit, Input } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../../environments/environment';
import { AuthService } from 'src/app/_services/auth.service';

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

   constructor(private authService: AuthService) {
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
         maxFileSize: 10 * 1024 * 1024,
         disableMultipart: true, // 'DisableMultipart' must be 'true' for formatDataFunction to be called.
         formatDataFunctionIsAsync: true,
         formatDataFunction: async item => {
            return new Promise((resolve, reject) => {
               resolve({
                  name: item._file.name,
                  length: item._file.size,
                  contentType: item._file.type,
                  date: new Date()
               });
            });
         }
      });

      this.hasBaseDropZoneOver = false;

      this.response = '';

      this.uploader.response.subscribe(res => (this.response = res));
   }

   public fileOverBase(e: any): void {
      this.hasBaseDropZoneOver = e;
   }

   ngOnInit() {}
}
