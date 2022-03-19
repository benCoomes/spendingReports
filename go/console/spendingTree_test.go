package main

import "testing"

func head() SpendingTreeNode {
	return SpendingTreeNode{
		children: make([]SpendingTreeNode, 0),
		vendors:  make([]Vendor, 0),
		name:     "head",
	}
}

func vendor1() Vendor {
	transaction := Transaction{
		amount:  -10.00,
		details: "vendor1_transaction1",
		date:    "2022-01-01",
	}

	return Vendor{
		name:         "vendor1",
		transactions: []Transaction{transaction},
		categories:   []string{"level_1_A"},
	}
}

func vendor2() Vendor {
	transaction := Transaction{
		amount:  -20.00,
		details: "vendor2_transaction1",
		date:    "2022-02-02",
	}

	return Vendor{
		name:         "vendor2",
		transactions: []Transaction{transaction},
		categories:   []string{"level_1_A"},
	}
}

func vendor3() Vendor {
	transaction := Transaction{
		amount:  -30.00,
		details: "vendor3_transaction1",
		date:    "2022-03_03",
	}

	return Vendor{
		name:         "vendor3",
		transactions: []Transaction{transaction},
		categories:   []string{"level_1_B"},
	}
}

func Test_AddVendor_SingleCategorySingleVendor(t *testing.T) {
	// given
	head := head()
	vendor1 := vendor1()

	// when
	head.AddVendor(vendor1)

	// then
	actualFirstLevel := head.children[0]
	checkNodeName(t, &actualFirstLevel, "actual first level", vendor1.categories[0])
	checkVendorExistsOnNode(t, &actualFirstLevel, "actual first level", vendor1.name)

	t.Logf("\n%v", head.PrettyPrint())
}

func Test_AddVendor_SingleCategoryTwoVendor(t *testing.T) {
	// given
	head := head()
	vendor1 := vendor1()
	vendor2 := vendor2()

	// when
	head.AddVendor(vendor1)
	head.AddVendor(vendor2)

	// then
	actualFirstLevel := head.children[0]
	checkNodeName(t, &actualFirstLevel, "actual first level", vendor1.categories[0])
	checkVendorExistsOnNode(t, &actualFirstLevel, "actual first level", vendor1.name)
	checkVendorExistsOnNode(t, &actualFirstLevel, "actual first level", vendor2.name)

	t.Logf("\n%v", head.PrettyPrint())
}

func Test_AddVendor_TwoCategoriesSingleVendor(t *testing.T) {
	// given
	head := head()
	vendor1 := vendor1()
	vendor3 := vendor3()

	// when
	head.AddVendor(vendor1)
	head.AddVendor(vendor3)

	// then
	actualFirstLevelA := head.children[0]
	actualFirstLevelB := head.children[1]
	checkNodeName(t, &actualFirstLevelA, "actual first level A", vendor1.categories[0])
	checkNodeName(t, &actualFirstLevelB, "actual first level B", vendor3.categories[0])

	checkVendorExistsOnNode(t, &actualFirstLevelA, "actual first level A", vendor1.name)
	checkVendorExistsOnNode(t, &actualFirstLevelB, "actual first level B", vendor3.name)

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
