package main

import (
	"fmt"
	"strings"
)

type SpendingTreeNode struct {
	name     string
	vendors  []*Vendor
	children []*SpendingTreeNode
}

// todo: these must be JSON support to accomplish the same thing - use it!
func (head SpendingTreeNode) PrettyPrint() string {
	return head.prettyPrint(0)
}

func (node *SpendingTreeNode) prettyPrint(level int) string {
	tabs := strings.Repeat("   ", level)
	theString := fmt.Sprintf("%v%v\n", tabs, node.name)

	tabs = tabs + "   "
	for i := range node.vendors {
		vendor := node.vendors[i]
		theString += fmt.Sprintf("%v%v: %v\n", tabs, vendor.name, vendor.Total())
	}
	for i := range node.children {
		theString += fmt.Sprintf("%v", node.children[i].prettyPrint(level+1))
	}

	return theString
}

func (head *SpendingTreeNode) AddVendor(vendor *Vendor) {
	head.addVendor(vendor, 0)
}

func (node *SpendingTreeNode) addVendor(vendor *Vendor, level int) {
	child, found := node.findChild(vendor.categories[level])

	if !found {
		child = newLevel(vendor.categories[level])
		node.children = append(node.children, child)
	}

	if len(vendor.categories) == level+1 { // todo: what about vendor with no categories?
		child.vendors = append(child.vendors, vendor)
	} else {
		child.addVendor(vendor, level+1)
	}
}

func (node *SpendingTreeNode) findChild(category string) (*SpendingTreeNode, bool) {
	for i := range node.children {
		if node.children[i].name == category {
			return node.children[i], true
		}
	}

	return nil, false
}

func newLevel(levelName string) *SpendingTreeNode {
	return &SpendingTreeNode{
		name:     levelName,
		vendors:  make([]*Vendor, 0),
		children: make([]*SpendingTreeNode, 0),
	}
}

// This is a non-TDD attempt
// compare to the final result, built with TDD!
// func (head *SpendingTreeNode) AddVendorAttempt1(vendor *Vendor) {
// 	head.addVendorAttempt2(vendor, 0)
// }

// func (node *SpendingTreeNode) addVendorAttempt2(vendor *Vendor, categoryIndex int) bool {
// 	isMatch := vendor.categories[categoryIndex] == node.name
// 	isLastCategory := categoryIndex >= len(vendor.categories)-1

// 	if isLastCategory {
// 		if isMatch {
// 			node.vendors = append(node.vendors, vendor)
// 			return true
// 		} else {
// 			for i := range node.children {
// 				added := node.children[i].addVendorAttempt2(vendor, categoryIndex)
// 				if added {
// 					return true
// 				}
// 			}

// 			newChild := SpendingTreeNode{
// 				name:     vendor.categories[categoryIndex],
// 				vendors:  make([]*Vendor, 0),
// 				children: make([]*SpendingTreeNode, 0),
// 			}
// 			node.children = append(node.children, &newChild)
// 			return newChild.addVendorAttempt2(vendor, categoryIndex)
// 		}
// 	} else {
// 		if isMatch {
// 			for i := range node.children {
// 				added := node.children[i].addVendorAttempt2(vendor, categoryIndex+1)
// 				if added {
// 					return true
// 				}
// 			}

// 			// no matching children, make a new one
// 			newChild := SpendingTreeNode{
// 				name:     vendor.categories[categoryIndex],
// 				vendors:  make([]*Vendor, 0),
// 				children: make([]*SpendingTreeNode, 0),
// 			}
// 			node.children = append(node.children, &newChild)
// 			return newChild.addVendorAttempt2(vendor, categoryIndex+1)
// 		} else {
// 			return false
// 		}
// 	}
// }
