import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Message } from '../_models/message';

const httpOptions = {
   headers: new HttpHeaders({
      Authorization: 'Bearer ' + localStorage.getItem('token'),
   }),
};

@Injectable({
   providedIn: 'root',
})
export class UserService {
   baseUrl = environment.apiUrl;
   constructor(private http: HttpClient) {}

   getUsers(
      page?,
      itemsPerPage?,
      userParams?,
      likesParams?
   ): Observable<PaginatedResult<User[]>> {
      const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<
         User[]
      >();

      let params = new HttpParams();

      if (page != null && itemsPerPage != null) {
         params = params.append('pageNumber', page);
         params = params.append('pageSize', itemsPerPage);
      }

      if (userParams != null) {
         params = params.append('minAge', userParams.minAge);
         params = params.append('maxAge', userParams.maxAge);
         params = params.append('gender', userParams.gender);
         params = params.append('orderBy', userParams.orderBy);
      }

      if (likesParams === 'Likers') {
         params = params.append('likers', 'true');
      }

      if (likesParams === 'Likees') {
         params = params.append('likees', 'true');
      }

      return this.http
         .get<User[]>(this.baseUrl + 'user', { observe: 'response', params })
         .pipe(
            map((response) => {
               paginatedResult.result = response.body;

               if (response.headers.get('Pagination') != null) {
                  paginatedResult.pagination = JSON.parse(
                     response.headers.get('Pagination')
                  );
               }

               return paginatedResult;
            })
         );
   }

   getUser(id: number): Observable<User> {
      return this.http.get<User>(this.baseUrl + 'user/' + id);
   }

   updateUser(id: number, user: User) {
      return this.http.put(this.baseUrl + 'user/' + id, user);
   }

   setMainPhoto(userId: number, photoId: number) {
      return this.http.post(
         this.baseUrl + 'user/' + userId + '/photos/' + photoId + '/setmain',
         {}
      );
   }

   deletePhoto(userId: number, photoId: number) {
      return this.http.delete(
         this.baseUrl + 'user/' + userId + '/photos/' + photoId
      );
   }

   sendLike(id: number, recipientId: number) {
      return this.http.post(
         this.baseUrl + 'user/' + id + '/like/' + recipientId,
         {}
      );
   }

   getMessages(userId: number, page?, itemsPerPage?, messageContainer?) {
      const paginatedResult: PaginatedResult<Message[]> = new PaginatedResult<
         Message[]
      >();

      let params = new HttpParams();

      params = params.append('MessageContainer', messageContainer);

      if (page != null && itemsPerPage != null) {
         params = params.append('pageNumber', page);
         params = params.append('pageSize', itemsPerPage);
      }

      return this.http
         .get<Message[]>(this.baseUrl + 'user/' + userId + '/message', {
            observe: 'response',
            params,
         })
         .pipe(
            map((response) => {
               paginatedResult.result = response.body;
               if (response.headers.get('Pagination') != null) {
                  paginatedResult.pagination = JSON.parse(
                     response.headers.get('Pagination')
                  );
               }

               return paginatedResult;
            })
         );
   }

   getMessageThread(userId: number, recipientId: number) {
      return this.http.get<Message[]>(
         this.baseUrl + 'user/' + userId + '/message/thread/' + recipientId
      );
   }

   sendMessage(userId: number, message: Message) {
      return this.http.post(
         this.baseUrl + 'user/' + userId + '/message',
         message
      );
   }
}
