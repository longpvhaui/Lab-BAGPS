import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserModel } from "src/app/pages/user/user.model";

@Injectable({
    providedIn: 'root'
})
export class UserService{
     apiUrl = 'https://localhost:7257/api/User/';
    constructor(private http:HttpClient){
        
    }
    getAllUser(){
        return this.http.get<UserModel>(this.apiUrl + 'get-all');
    }
}