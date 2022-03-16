import { Transaction } from "./transaction";

export class TransactionList {
    constructor() {
        this.message = "Wow you don't have any transactions?"
    }

    created(owningView, myView) {
       this.loadTransactions();
    }

    loadTransactions()
    {
        fetch('http://localhost:8080/transaction')
            .then(res => res.json())
            .then(serverData => {
                this.transactions = serverData.map(serverModel => new Transaction(serverModel));
            })
            .catch((error) => {
                console.error('Error loading transactions:', error);
            });
    }
}