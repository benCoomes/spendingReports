import { observable } from "aurelia-framework";

export class CategoryList {
    @observable searchTextInputFocus = false;
    @observable categoryInputFocus = false;

    constructor() {
        this.message = 'Category Summary';
        this.classifiers = [];
    }

    bind() {
        this.getClassifiers();
    }

    getClassifiers() {
        fetch("http://localhost:8080/classifier")
        .then(res => res.json())
        .then(serverData => {
            this.classifiers = serverData.map(serverModel => {
                return {
                    category: serverModel.category,
                    searchText: serverModel.searchValue
                };
            });
        })
        .catch((error) => {
            console.error('Error loading classifiers:', error);
        })
    }

    postNewClassifier() {
        fetch("http://localhost:8080/classifier", {
            method: "POST",
            body: JSON.stringify({
                category: this.newCategory,
                searchValue: this.newSearchText
            }),
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        })
        .then(res => res.json())
        .then(newServerClassifier => {
            this.classifiers.push({
                category: newServerClassifier.category,
                searchText: newServerClassifier.searchValue
            });
            this.newCategory = "";
            this.newSearchText = "";
        })
        .catch(error => {
            alert("Error saving new classifier.");
            console.error('Failed to create new classifier:', error);
        });
    }

    categoryInputFocusChanged(newFocus, oldFocus) {
        this.addClassifierIfInputFinished();
    }

    searchTextInputFocusChanged(newFocus, oldFocus) {
        this.addClassifierIfInputFinished();
    }

    addClassifierIfInputFinished() {
        if(!this.searchTextInputFocus 
            && !this.categoryInputFocus
            && this.newSearchText
            && this.newCategory)
        {
            this.postNewClassifier();
        }
    }
}