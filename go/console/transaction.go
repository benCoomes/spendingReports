package main

import (
	"errors"
	"fmt"
	"strings"
)

type Transaction struct {
	date    string
	amount  float64
	details string
}

func (t Transaction) String() string {
	return fmt.Sprintf("%v %v %v", t.date, t.amount, t.details)
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

	amount, err := ParseFloat64(segments[1])
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
