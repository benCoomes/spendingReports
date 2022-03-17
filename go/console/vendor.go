package main

import (
	"errors"
	"fmt"
	"regexp"
	"strings"
)

type Vendor struct {
	matcher      *regexp.Regexp
	name         string
	categories   []string
	transactions []Transaction
}

func (v Vendor) String() string {
	return fmt.Sprintf("%v, %v, [%v]", v.name, v.matcher, strings.Join(v.categories, ";"))
}

func (v Vendor) Matches(t Transaction) bool {
	return v.matcher.FindStringIndex(t.details) != nil
}

// use a pointer, because modifying a copy is pointless
func (v *Vendor) TryAdd(t Transaction) bool {
	if v.Matches(t) {
		v.transactions = append(v.transactions, t)
		return true
	} else {
		return false
	}
}

func (v *Vendor) Total() float32 {
	var sum float32 = 0.0
	for _, transaction := range v.transactions {
		sum = sum + transaction.amount
	}
	return sum
}

const expectedVendorSegments = 3

func ParseVendor(raw string) (Vendor, error) {
	segments := strings.Split(raw, ",")
	actualSegments := len(segments)
	if actualSegments != expectedVendorSegments {
		errtext := fmt.Sprintf("Expected %v items in raw string but found %v", expectedVendorSegments, expectedSegments)
		return Vendor{}, errors.New(errtext)
	}

	name, err := ParseString(segments[0])
	if err != nil {
		return Vendor{}, err
	}

	matcher, err := ParseRegex(segments[1])
	if err != nil {
		return Vendor{}, err
	}

	categories := ParseStringSlice(segments[2], ";")

	return Vendor{
		name:       name,
		matcher:    matcher,
		categories: categories,
	}, nil
}
