export class Transaction {
    constructor(serverModel) {
        this._date = serverModel?.date ? new Date(serverModel.date) : undefined;
        this.amount = serverModel?.amount;
        this.category = serverModel?.category;
        this.description = serverModel?.description;
    }

    get date() {
        if(this._date) {
            return this._date.toLocaleDateString() + ' ' + this._date.toLocaleTimeString();
        }
        else {
            return undefined;
        }
    }
}