import { Component, ElementRef } from '@angular/core';
import { FileUploaderService } from '../../services/file-uploader.service';

@Component({
    selector: 'file-upload',
    templateUrl: './file-uploader.component.html',
    styles: [`
      .spinner{
        visibility:hidden;
      }
  `],
    providers: [FileUploaderService]
})
export class FileUploaderComponent {

    public isFileUploaded: boolean;
    public errorMessage: string = '';

    constructor(private fileUploader: FileUploaderService, private elem: ElementRef) {

    }

    public uploadFile(): void {
        this.isFileUploaded = false;
        this.errorMessage = '';
        
        let files = this.elem.nativeElement.querySelector('#selectFile').files;
        let formData = new FormData();
        let file = files[0];

        if (file) {
            this.elem.nativeElement.querySelector('#spinner').style.visibility = 'visible';
            formData.append('selectFile', file, file.name);
            this.fileUploader.uploadFile(formData)
                .subscribe(
                (res) => this.dataLoaded(res),
                (error) => {
                    this.elem.nativeElement.querySelector('#spinner').style.visibility = 'hidden';
                    this.errorMessage = error._body;
                    if (error.status === 401)
                        this.errorMessage = 'Administrators can only upload files.'
                }
                );
        }
        else
            this.errorMessage = "Please choose a .csv file to upload balances."
    }

    private dataLoaded(data: any): void {
        this.errorMessage = '';
        this.elem.nativeElement.querySelector('#spinner').style.visibility = 'hidden';
        this.isFileUploaded = true;
    }

    chooseFile(filePath: string) {
        this.isFileUploaded = false;
        if (filePath !== '')
            this.errorMessage = '';
    }
}
