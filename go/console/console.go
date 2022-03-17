package main

import (
	"errors"
	"fmt"
	"os"
	"path/filepath"
	"regexp"
	"strconv"
	"strings"
)

func main() {
	transactionFile := "../testTransactions.csv"
	vendorFile := "../testVendors.csv"

	transactions := ReadTransactionsFromFile(transactionFile)
	vendors := ReadVendorsFromFile(vendorFile)

	fmt.Println("Vendors:")
	for _, vendor := range vendors {
		fmt.Println(vendor)
	}

	fmt.Println("\nTransactions:")
	for _, transaction := range transactions {
		vendor, foundMatch := MatchVendor(vendors, transaction)
		if foundMatch {
			fmt.Printf("Transaction \"%v\" matched to vendor \"%v\"\n", transaction, vendor.name)
		} else {
			fmt.Printf("Transaction \"%v\" did not match any vendor.\n", transaction)
		}
	}

}

type Transaction struct {
	date    string
	amount  float32
	details string
}

func (t Transaction) String() string {
	return fmt.Sprintf("[%v] [%v] [%v]", t.date, t.amount, t.details)
}

type Vendor struct {
	matcher *regexp.Regexp
	name    string
}

func (v Vendor) String() string {
	return fmt.Sprintf("[%v], [%v]", v.name, v.matcher)
}

func (v Vendor) Matches(t Transaction) bool {
	return v.matcher.FindStringIndex(t.details) == nil
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func MatchVendor(vendors []Vendor, transaction Transaction) (Vendor, bool) {
	for _, vendor := range vendors {
		if vendor.Matches(transaction) {
			return vendor, true
		}
	}
	return Vendor{}, false
}

func ReadVendorsFromFile(path string) []Vendor {
	contents := ReadAsString(path)
	lines := strings.Split(contents, "\n")

	var vendors []Vendor

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

const expectedVendorSegments = 2

func ParseVendor(raw string) (Vendor, error) {
	segments := strings.Split(raw, ",")
	actualSegments := len(segments)
	if actualSegments != expectedVendorSegments {
		errtext := fmt.Sprintf("Expected %v items in raw string but found %v", expectedSegments, expectedVendorSegments)
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

	return Vendor{
		name:    name,
		matcher: matcher,
	}, nil
}

func ReadTransactionsFromFile(path string) []Transaction {

	contents := ReadAsString(path)
	lines := strings.Split(contents, "\n")

	var transactions []Transaction

	for _, line := range lines {
		transaction, err := ParseTransaction(line)
		if err != nil {
			fmt.Println(err)
		} else {
			transactions = append(transactions, transaction)
		}
	}

	return transactions
}

const expectedSegments = 3

func ParseTransaction(raw string) (Transaction, error) {
	segments := strings.Split(raw, ",")
	actualSegments := len(segments)
	if actualSegments != expectedSegments {
		errtext := fmt.Sprintf("Expected %v items in raw string but found %v", expectedSegments, actualSegments)
		return Transaction{}, errors.New(errtext)
	}

	date, err := ParseDate(segments[0])
	if err != nil {
		return Transaction{}, err
	}

	amount, err := ParseFloat32(segments[1])
	if err != nil {
		return Transaction{}, err
	}

	details, err := ParseString(segments[2])
	if err != nil {
		return Transaction{}, err
	}

	return Transaction{
		date:    date,
		amount:  amount,
		details: details,
	}, nil
}

func ParseDate(dateString string) (string, error) {
	return strings.TrimSpace(dateString), nil
}

func ParseFloat32(amountString string) (float32, error) {
	amountString = strings.TrimSpace(amountString)

	amount, err := strconv.ParseFloat(amountString, 32)
	if err != nil {
		errtext := fmt.Sprintf("Failed to parse amount into a number. String value: %v", amountString)
		return 0, errors.New(errtext)
	}

	return float32(amount), nil
}

func ParseString(detailsString string) (string, error) {
	return strings.TrimSpace(detailsString), nil
}

func ParseRegex(regexString string) (*regexp.Regexp, error) {
	regexString = strings.TrimSpace(regexString)

	regex, err := regexp.Compile(regexString)
	if err != nil {
		errtext := fmt.Sprintf("Failed to parse into a regular expression. String value: %v", regexString)
		return nil, errors.New(errtext)
	}

	return regex, nil
}

func ReadAsString(path string) string {
	absPath, err := filepath.Abs(path)
	check(err)

	contents, err := os.ReadFile(absPath)
	check(err)

	return string(contents)
}
