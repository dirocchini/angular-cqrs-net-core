import { Photo } from './photo';

export interface User {
   id: number;
   name: string;
   email: string;
   login: string;
   userName: string;
   gender: string;
   age: number;
   dateOfBirth: Date;
   knownAs: string;
   lastActive: Date;
   city: string;
   country: string;
   created: Date;

   photoUrl?: string;
   introduction?: string;
   lookingFor?: string;
   interests?: string;
   photos: Photo[];
   roles?: string[];
}
