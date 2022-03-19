package main

import "testing"

func head() SpendingTreeNode {
	return SpendingTreeNode{
		children: make([]SpendingTreeNode, 0),
		vendors:  make([]Vendor, 0),
		name:     "head",
	}
}

func vendor_1A_1() Vendor {
	transaction := Transaction{
		amount:  -10.00,
		details: "vendor_1A_1_transaction_1",
		date:    "2022-01-01",
	}

	return Vendor{
		name:         "vendor_1A_1",
		transactions: []Transaction{transaction},
		categories:   []string{"level_1_A"},
	}
}

func vendor_1A_2() Vendor {
	transaction := Transaction{
		amount:  -20.00,
		details: "vendor_1A_2_transaction_1",
		date:    "2022-02-02",
	}

	return Vendor{
		name:         "vendor_1A_2",
		transactions: []Transaction{transaction},
		categories:   []string{"level_1_A"},
	}
}

func vendor_1B_1() Vendor {
	transaction := Transaction{
		amount:  -30.00,
		details: "vendor_1B_1_transaction_1",
		date:    "2022-03_03",
	}

	return Vendor{
		name:         "vendor_1B_1",
		transactions: []Transaction{transaction},
		categories:   []string{"level_1_B"},
	}
}

func Test_AddVendor_SingleCategorySingleVendor(t *testing.T) {
	// given
	head := head()
	vendor1A1 := vendor_1A_1()

	// when
	head.AddVendor(vendor1A1)

	// then
	actualFirstLevel := head.children[0]
	checkNodeName(t, &actualFirstLevel, "actual first level", vendor1A1.categories[0])
	checkVendorExistsOnNode(t, &actualFirstLevel, "actual first level", vendor1A1.name)

	t.Logf("\n%v", head.PrettyPrint())
}

func Test_AddVendor_SingleCategoryTwoVendor(t *testing.T) {
	// given
	head := head()
	vendor1A1 := vendor_1A_1()
	vendor1A2 := vendor_1A_2()

	// when
	head.AddVendor(vendor1A1)
	head.AddVendor(vendor1A2)

	// then
	actualFirstLevel := head.children[0]
	checkNodeName(t, &actualFirstLevel, "actual first level", vendor1A1.categories[0])
	checkVendorExistsOnNode(t, &actualFirstLevel, "actual first level", vendor1A1.name)
	checkVendorExistsOnNode(t, &actualFirstLevel, "actual first level", vendor1A2.name)

	t.Logf("\n%v", head.PrettyPrint())
}

func Test_AddVendor_TwoCategoriesSingleVendor(t *testing.T) {
	// given
	head := head()
	vendor1A1 := vendor_1A_1()
	vendor1B1 := vendor_1B_1()

	// when
	head.AddVendor(vendor1A1)
	head.AddVendor(vendor1B1)

	// then
	actualFirstLevelA := head.children[0]
	actualFirstLevelB := head.children[1]
	checkNodeName(t, &actualFirstLevelA, "actual first level A", vendor1A1.categories[0])
	checkNodeName(t, &actualFirstLevelB, "actual first level B", vendor1B1.categories[0])

	checkVendorExistsOnNode(t, &actualFirstLevelA, "actual first level A", vendor1A1.name)
	checkVendorExistsOnNode(t, &actualFirstLevelB, "actual first level B", vendor1B1.name)

	t.Logf("\n%v", head.PrettyPrint())
}

func checkNodeName(t *testing.T, node *SpendingTreeNode, nodeDescription string, expectedName string) {
	if node.name != expectedName {
		t.Fatalf("Expected %v to have name '%v' but found '%v'", nodeDescription, expectedName, node.name)
	}
}

func checkVendorExistsOnNode(t *testing.T, node *SpendingTreeNode, nodeDescription string, expectedVendorName string) {
	foundNames := make([]string, 0)
	for i := range node.vendors {
		actualVendorName := node.vendors[i].name
		if actualVendorName == expectedVendorName {
			return
		}
		foundNames = append(foundNames, actualVendorName)
	}

	t.Fatalf("Expected %v to have a vendor with name '%v', but found only %v", nodeDescription, expectedVendorName, foundNames)
}
