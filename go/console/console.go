package main

import (
	"fmt"
)

func main() {
	transactionFile := "../testTransactions.csv"
	vendorFile := "../testVendors.csv"

	transactions := ReadTransactionsFromFile(transactionFile)
	vendors := CreateVendorListFromFile(vendorFile)

	fmt.Println("Vendors:")
	for _, vendor := range vendors {
		fmt.Println(vendor)
	}

	fmt.Println("\nTransactions:")
	for _, transaction := range transactions {
		vendor, foundMatch := vendors.AddToMatchingVendor(transaction)
		if foundMatch {
			fmt.Printf("Transaction \"%v\" matched to vendor \"%v\"\n", transaction, vendor.name)
		} else {
			fmt.Printf("Transaction \"%v\" did not match any vendor.\n", transaction)
		}
	}

	fmt.Println("\nNet Totals:")
	for _, vendor := range vendors {
		fmt.Printf("%v: %v\n", vendor.name, vendor.Total())
	}

	tree := SpendingTreeNode{
		name:     "Spending Tree",
		vendors:  make([]Vendor, 0),
		children: make([]SpendingTreeNode, 0),
	}
	for i := range vendors {
		tree.AddVendorAttempt1(vendors[i])
	}
	fmt.Printf("\n\n%v\n\n", tree)
	fmt.Printf("\n\n%v\n\n", tree.PrettyPrint())

}
