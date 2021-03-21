export class CategoryList {
    constructor() {
        this.message = 'Category Summary'
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
}