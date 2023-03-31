import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserSearch } from "src/app/pages/user/user-show/user-search.model";
import { UserModel } from "src/app/pages/user/user.model";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})
export class UserService{

    constructor(private http:HttpClient){
        
    }
    getUser(pageIndex:number,pageSize:number){
      
        return this.http.get<UserModel>(environment.apiUrl + '/api/User/get-all?'+ `pageIndex=${pageIndex}&pageSize=${pageSize}`);
    }
    addUser(user:UserModel){
        return this.http.post<string>(environment.apiUrl + '/api/User/add',user);
    }
    updateUser(user:UserModel){
        return this.http.post<UserModel>(environment.apiUrl + '/api/User/edit',user);
    }
    deleteUser(userId:number){
        return this.http.delete(environment.apiUrl + '/api/User/delete?' +`id=${userId}`);
    }

    getPagging(userSearch:UserSearch){
        return this.http.post(environment.apiUrl + '/api/User/get-search',userSearch);
    }
    deleteMultiUser(ids:number[]){
        return this.http.post(environment.apiUrl + '/api/User/delete-multi',ids)
    }
}