package main

import (
	"fmt"
	"os"
)

func main() {
	if len(os.Args) < 3 {
		fmt.Println("Missing required arguments. Usage: spendtool [vendorFile] [transactionFile]")
		return
	}
	vendorFile := os.Args[1]
	transactionFile := os.Args[2]

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
		name:     "Total Spending",
		vendors:  make([]*Vendor, 0),
		children: make([]*SpendingTreeNode, 0),
	}
	for i := range vendors {
		tree.AddVendor(&vendors[i])
	}
	fmt.Printf("\n\n%v\n\n", tree.PrettyPrint())

}
