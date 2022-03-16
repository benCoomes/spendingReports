package main

import (
	"errors"
	"fmt"
	"os"
	"path/filepath"
	"strconv"
	"strings"
)

func main() {
	filename := "../testTransactions.csv"

	transactions := ReadTransactionsFromFile(filename)

	for _, transaction := range transactions {
		fmt.Println(transaction)
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

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func ReadTransactionsFromFile(path string) []Transaction {

	var contents = ReadAsString(path)
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

	amount, err := ParseAmount(segments[1])
	if err != nil {
		return Transaction{}, err
	}

	details, err := ParseDetails(segments[2])
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

func ParseAmount(amountString string) (float32, error) {
	amountString = strings.TrimSpace(amountString)

	amount, err := strconv.ParseFloat(amountString, 32)
	if err != nil {
		errtext := fmt.Sprintf("Failed to parse amount into a number. String value: %v", amountString)
		return 0, errors.New(errtext)
	}

	return float32(amount), nil
}

func ParseDetails(detailsString string) (string, error) {
	return strings.TrimSpace(detailsString), nil
}

func ReadAsString(path string) string {
	absPath, err := filepath.Abs(path)
	check(err)

	contents, err := os.ReadFile(absPath)
	check(err)

	return string(contents)
}
