export class Import {
    constructor() {
        this.errorMessage = "";
    }

    uploadFiles() {
        this.upload('http://localhost:8080/import', this.selectedFiles)
            .then((response) => {
                if(!response.ok) {
                    this.errorMessage = "There was an error uploading the file.";
                } else {
                    this.clearFiles()
                    this.errorMessage = "";
                }
            });
    }

    clearFiles() {
        document.getElementById("files").value = "";
    }

    upload(url, files, method="POST") {
        let formData = new FormData();
        for(let i = 0; i < files.length; i++) {
            formData.append(`files[${i}]`, files[i]);
        }

        return fetch(url, {
            method: method,
            body: formData,
            headers: new Headers()
        });
    }
}