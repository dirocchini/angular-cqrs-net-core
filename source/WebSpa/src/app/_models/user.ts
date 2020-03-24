import { Photos } from './photos';

export interface User {
   id: number;
   name: string;
   email: string;
   login: string;
   gender: string;
   age: number;
   dateOfBirth: Date;
   knownAs: string;
   lastActive: Date;
   city: string;
   country: string;

   photoUrl?: string;
   introduction?: string;
   lookingFor?: string;
   interests?: string;
   photos: Photos[];
}
