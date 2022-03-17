package main

import (
	"fmt"
	"strings"
)

type VendorList []Vendor

func AddToVendor(vendors VendorList, transaction Transaction) (Vendor, bool) {
	// use index because value returns a copy, and modifying a copy is pointless
	for i := range vendors {
		if vendors[i].TryAdd(transaction) {
			return vendors[i], true
		}
	}
	return Vendor{}, false
}

func ReadVendorsFromFile(path string) VendorList {
	contents := ReadAsString(path)
	lines := strings.Split(contents, "\n")

	var vendors VendorList

	for _, line := range lines {
		vendor, err := ParseVendor(line)
		if err != nil {
			fmt.Println(err)
		} else {
			vendors = append(vendors, vendor)
		}
	}

	return vendors
}
