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

func (v *Vendor) Total() float64 {
	sum := 0.0
	for _, transaction := range v.transactions {
		sum = sum + transaction.amount
	}
	return sum
}

const expectedVendorSegments = 3

func ParseVendor(raw string) (Vendor, error) {
	if strings.TrimSpace(raw) == "" {
		return Vendor{}, errors.New("raw string cannot be empty or only whitespace")
	}

	segments := strings.Split(raw, ",")
	actualSegments := len(segments)
	if actualSegments != expectedVendorSegments {
		errtext := fmt.Sprintf("expected %v items in raw vendor string but found %v. Raw string: %v", expectedVendorSegments, expectedSegments, raw)
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
