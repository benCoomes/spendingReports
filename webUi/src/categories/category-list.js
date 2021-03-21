import { observable } from "aurelia-framework";

export class CategoryList {
    @observable searchTextInputFocus = false;
    @observable categoryInputFocus = false;

    constructor() {
        this.message = 'Category Summary';
        this.classifiers = [];
    }

    getClassifiers() {
        fetch("http://localhost:8080/classifier")
        .then(res => res.json())
        .then(serverData => {
            this.classifiers = serverData.map(serverModel => new this.classifiers(serverModel))
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
                searchText: this.newSearchText
            }),
            headers: new Headers()
        })
        .then(res => res.json())
        .then(newServerClassifier => {
            this.classifiers.push({
                category: newServerClassifier.category,
                searchText: newServerClassifier.searchText
            });
            this.newCategory = "";
            this.newSearchText = "";
        })
        .catch(error => {
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
            this.classifiers.push({
                category: this.newCategory,
                searchText: this.newSearchText
            });
            this.newCategory = "";
            this.newSearchText = "";
        }
    }
}