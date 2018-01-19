import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { AuthenticationService } from "./authentication.service";
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';


@Injectable()
export class FileUploaderService {

    private readonly fileUploadUrl: string = '/api/FileUpload/Post';

    constructor(private _http: Http, private auhenticationService: AuthenticationService) {

    }

    public uploadFile(formdata: any) {
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.auhenticationService.getToken() });
        let options = new RequestOptions({ headers: headers });

        return this._http.post(this.fileUploadUrl, formdata, options)
            .catch(this._errorHandler);
    }

    private _errorHandler(error: Response) {
        console.error('Error Occured: ' + error);
        return Observable.throw(error || 'Error Occured on Server');
    }
}